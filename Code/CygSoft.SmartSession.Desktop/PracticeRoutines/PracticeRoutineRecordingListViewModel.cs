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

        public RelayCommand PracticeCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

        public BindingList<RecordableExerciseViewModel> RecordableExercises { get; set; } = new BindingList<RecordableExerciseViewModel>();

        public PracticeRoutineRecordingListViewModel(IPracticeRoutineService practiceRoutineService,  IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Practice Routine Service must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Exercise Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            PracticeCommand = new RelayCommand(Practice, () => true);
            ExitCommand = new RelayCommand(Exit, () => true);
        }

        public void InitializeSession(PracticeRoutine practiceRoutine)
        {
            foreach (var exercise in practiceRoutine.PracticeRoutineExercises)
            { }
        }

        private void Exit()
        {
            Messenger.Default.Send(new ExitPracticeListMessage());
        }

        private void Practice()
        {
            Messenger.Default.Send(new ExitPracticeListMessage());
        }
    }
}
