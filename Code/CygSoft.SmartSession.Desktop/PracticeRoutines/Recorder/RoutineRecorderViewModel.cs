using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder
{
    public class RoutineRecorderViewModel : ViewModelBase
    {
        private IPracticeRoutineService practiceRoutineService;
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand StartExercisingCommand { get; private set; }

        public BindingList<TimeSlotRecorderViewModel> RecordableExercises { get; set; } = new BindingList<TimeSlotRecorderViewModel>();

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
            }
        }

        public bool HasSelection { get => SelectedRecordableExercise != null; }

        private TimeSlotRecorderViewModel selectedRecordableExercise;
        public TimeSlotRecorderViewModel SelectedRecordableExercise
        {
            get { return selectedRecordableExercise; }
            set
            {
                Set(() => SelectedRecordableExercise, ref selectedRecordableExercise, value);
                RaisePropertyChanged(() => HasSelection);
            }
        }

        private string displayTime;
        public string DisplayTime
        {
            get
            {
                return displayTime;
            }
            set
            {
                Set(() => DisplayTime, ref displayTime, value);
            }
        }

        public bool Recording
        {
            get
            {
                if (RecordableExercises != null)
                    return RecordableExercises.Where(re => re.Recording == true).Any();
                return false;
            }
       }

        private double totalSecondsPracticed;
        public double TotalSecondsPracticed
        {
            get
            {
                return totalSecondsPracticed;
            }
            set
            {
                Set(() => TotalSecondsPracticed, ref totalSecondsPracticed, value);
            }
        }

        public RoutineRecorderViewModel(IPracticeRoutineService practiceRoutineService,  IExerciseService exerciseService, IDialogViewService dialogService)
        {
            RecordableExercises.ListChanged += RecordableExercises_ListChanged;
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Practice Routine Service must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Exercise Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            CancelCommand = new RelayCommand(() => Cancel(), () => !Recording);
            SaveCommand = new RelayCommand(() => Save(), () => CanExecuteSaveCommand());
            StartExercisingCommand = new RelayCommand(StartExercising, () => true);
        }

        private bool CanExecuteSaveCommand()
        {
            return TotalSecondsPracticed > 0 && !Recording;
        }

        private void RecordableExercises_ListChanged(object sender, ListChangedEventArgs e)
        {
            TotalSecondsPracticed = (int)RecordableExercises.Sum(r => r.Seconds);
            DisplayTime = TimeFuncs.DisplayTimeFromSeconds(TotalSecondsPracticed);
            RaisePropertyChanged(() => Recording);

            if (SaveCommand != null)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => SaveCommand.RaiseCanExecuteChanged()));
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => CancelCommand.RaiseCanExecuteChanged()));
            }  
        }

        public void InitializeSession(int practiceRoutineId)
        {
            RecordableExercises.Clear();
            RaisePropertyChanged(() => HasSelection);

            var routineRecorder = practiceRoutineService.GetPracticeRoutineRecorder(practiceRoutineId);
            Title = routineRecorder.Title;

            foreach (var exerciseRecorder in routineRecorder.ExerciseRecorders)
            {
                RecordableExercises.Add(new TimeSlotRecorderViewModel(exerciseService, exerciseRecorder));
            }
            
        }

        private void Save()
        { 
            foreach (var recordableExercise in RecordableExercises)
            {
                if (recordableExercise.Seconds > 0)
                {
                    recordableExercise.SaveRecording(exerciseService);
                }
            }

            Messenger.Default.Send(new ExitPracticeListMessage());
        }

        private void Cancel()
        {
            if (dialogService.YesNoPrompt("Cancel Session?", "Are you sure you want to cancel the session? All data will be lost!"))
                Messenger.Default.Send(new ExitPracticeListMessage());
        }

        private void StartExercising()
        {
            if (SelectedRecordableExercise.Recording)
                SelectedRecordableExercise.Pause();
            else
            {
                foreach (var item in RecordableExercises)
                {
                    if (item.Recording) item.Pause();
                }
                
                SelectedRecordableExercise.Start();
            }
        }
    }
}
