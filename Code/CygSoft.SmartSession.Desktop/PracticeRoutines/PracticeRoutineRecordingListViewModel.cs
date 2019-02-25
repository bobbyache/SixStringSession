using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{

    public class PracticeRoutineRecordingListViewModel : ViewModelBase
    {
        private IPracticeRoutineService practiceRoutineService;
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand StartExercisingCommand { get; private set; }

        public BindingList<RecordableExerciseViewModel> RecordableExercises { get; set; } = new BindingList<RecordableExerciseViewModel>();

        private RecordableExerciseViewModel selectedRecordableExercise;
        public RecordableExerciseViewModel SelectedRecordableExercise
        {
            get { return selectedRecordableExercise; }
            set
            {
                Set(() => SelectedRecordableExercise, ref selectedRecordableExercise, value);
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

        //private bool recording;
        //public bool Recording
        //{
        //    get
        //    {
        //        return recording;
        //    }
        //    set
        //    {
        //        Set(() => Recording, ref recording, value);
        //    }
        //}

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

        public PracticeRoutineRecordingListViewModel(IPracticeRoutineService practiceRoutineService,  IExerciseService exerciseService, IDialogViewService dialogService)
        {
            RecordableExercises.ListChanged += RecordableExercises_ListChanged;
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Practice Routine Service must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Exercise Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            //CancelCommand = new RelayCommand(() => Cancel(), () => CanCancel());
            //SaveCommand = new RelayCommand(() => Save(), () => CanSave());
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
            SaveCommand = new RelayCommand(() => Save(), () => true);
            StartExercisingCommand = new RelayCommand(StartExercising, () => true);
        }

        //private bool CanCancel()
        //{
        //    return (!this.Recording);
        //}

        //private bool CanSave()
        //{
        //    return (!this.Recording && this.TotalSecondsPracticed > 0);
        //}

        private void RecordableExercises_ListChanged(object sender, ListChangedEventArgs e)
        {
            TotalSecondsPracticed = (int)RecordableExercises.Sum(r => r.Seconds);
            DisplayTime = TimeFuncs.DisplayTimeFromSeconds(TotalSecondsPracticed);
            //Recording = RecordableExercises.Any(ex => ex.Recording == true);
        }

        public void InitializeSession(int practiceRoutineId)
        {
            RecordableExercises.Clear();
            var exercises = exerciseService.GetPracticeRoutineExercises(practiceRoutineId);
            foreach (var exercise in exercises)
            {
                RecordableExercises.Add(new RecordableExerciseViewModel(exercise));
            }
        }

        private void Save()
        { 
            foreach (var recordableExercise in RecordableExercises)
            {
                if (recordableExercise.Seconds > 0)
                {
                    var exerciseActivity = exerciseService.CreateExerciseActivity(recordableExercise.MetronomeSpeed, 
                        (int)recordableExercise.Seconds, recordableExercise.ManualProgress, DateTime.Now, DateTime.Now);
                    recordableExercise.Exercise.ExerciseActivity.Add(exerciseActivity);
                }
            }

            var exercises = RecordableExercises.Select(r => r.Exercise);
            exerciseService.Update(exercises.OfType<Exercise>());

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
