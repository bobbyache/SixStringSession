using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
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

        public RelayCommand StartExercisingCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

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

        public PracticeRoutineRecordingListViewModel(IPracticeRoutineService practiceRoutineService,  IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Practice Routine Service must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Exercise Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            StartExercisingCommand = new RelayCommand(StartExercising, () => true);
            ExitCommand = new RelayCommand(Exit, () => true);
        }

        public void InitializeSession(int practiceRoutineId)
        {
            var exercises = exerciseService.GetPracticeRoutineExercises(practiceRoutineId);
            foreach (var exercise in exercises)
            {
                RecordableExercises.Add(new RecordableExerciseViewModel(exercise));
            }
        }

        private void Exit()
        {
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

            //Messenger.Default.Send(new ExitPracticeListMessage());
        }
    }
}
