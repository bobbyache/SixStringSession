using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineCompositeViewModel : ViewModelBase
    {
        private PracticeRoutineSearchViewModel practiceRoutineSearchViewModel;
        private PracticeRoutineEditViewModel practiceRoutineEditViewModel;
        private ExerciseSelectionViewModel exerciseSelectionViewModel;
        private PracticeRoutineRecorderViewModel practiceRoutineRecorderViewModel;
        private IPracticeRoutineService practiceRoutineService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public PracticeRoutineCompositeViewModel(IPracticeRoutineService practiceRoutineService, PracticeRoutineSearchViewModel practiceRoutineSearchViewModel, 
            PracticeRoutineEditViewModel practiceRoutineEditViewModel, ExerciseSelectionViewModel exerciseSelectionViewModel, PracticeRoutineRecorderViewModel practiceRoutineRecorderViewModel)
        {
            this.practiceRoutineService = practiceRoutineService;
            this.practiceRoutineSearchViewModel = practiceRoutineSearchViewModel;
            this.practiceRoutineEditViewModel = practiceRoutineEditViewModel;
            this.exerciseSelectionViewModel = exerciseSelectionViewModel;
            this.practiceRoutineRecorderViewModel = practiceRoutineRecorderViewModel;

            Messenger.Default.Register<CancelRecordingPracticeRoutineMessage>(this, (m) => CancelRecordingPracticeRoutine());
            Messenger.Default.Register<SavePracticeRoutineRecordingMessage>(this, (m) => SavePracticeRoutineRecording());

            Messenger.Default.Register<ExerciseSelectionCancelledMessage>(this, (m) => ExerciseSelectionCancelled());
            Messenger.Default.Register<ExerciseSelectedMessage>(this, (m) => ExerciseSelected(m.ExerciseId));
            Messenger.Default.Register<StartPracticingRoutineMessage>(this, (m) => StartPracticingRoutine(m.ExercisePracticeRoutineId));
            Messenger.Default.Register<StartSelectingPracticeRoutineExerciseMessage>(this, (m) => StartSelectingPracticeRoutine());
            Messenger.Default.Register<StartEditingPracticeRoutineMessage>(this, (m) => StartEditingPracticeRoutine(m.PracticeRoutine));
            Messenger.Default.Register<EndEditingPracticeRoutineMessage>(this, (m) => EndEditingPracticeRoutine(m.PracticeRoutine, m.Operation, m.LifeCycleState));

            NavigationCommand = new RelayCommand<string>(NavigateTo);
            NavigateTo("Search");
        }

        private void SavePracticeRoutineRecording()
        {
            NavigateTo("Search");
        }

        private void CancelRecordingPracticeRoutine()
        {
            NavigateTo("Search");
        }

        private void StartPracticingRoutine(int exercisePracticeRoutineId)
        {
            NavigateTo("Record");
        }

        private void ExerciseSelected(int exerciseId)
        {
            var routineExercise = practiceRoutineService.CreatePracticeRoutineExerciseFor(exerciseId);
            practiceRoutineEditViewModel.AddPracticeRoutineExercise(routineExercise);
            NavigateTo("Edit");
        }

        private void ExerciseSelectionCancelled()
        {
            NavigateTo("Edit");
        }

        private void StartSelectingPracticeRoutine()
        {
            NavigateTo("SelectExercise");
        }

        protected virtual void EndEditingPracticeRoutine(PracticeRoutine practiceRoutine, EditorCloseOperation operation,
            EntityLifeCycleState entityLifeCycleState)
        {
            if (operation == EditorCloseOperation.Saved)
            {
                if (entityLifeCycleState == EntityLifeCycleState.AsExistingEntity)
                    practiceRoutineService.Update(practiceRoutine);
                else
                    practiceRoutineService.Add(practiceRoutine);
            }

            NavigateTo("Search");
        }


        private void StartEditingPracticeRoutine(PracticeRoutine practiceRoutine)
        {
            practiceRoutineEditViewModel.StartEdit(practiceRoutine);
            NavigateTo("Edit");
        }

        private void NavigateTo(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = practiceRoutineSearchViewModel;
                    practiceRoutineSearchViewModel.RefreshRoutines();
                    break;
                case "Edit":
                    CurrentViewModel = practiceRoutineEditViewModel;
                    break;
                case "SelectExercise":
                    CurrentViewModel = exerciseSelectionViewModel;
                    break;
                case "Record":
                    CurrentViewModel = practiceRoutineRecorderViewModel;
                    break;
                default:
                    CurrentViewModel = practiceRoutineSearchViewModel;
                    break;
            }
        }
    }
}
