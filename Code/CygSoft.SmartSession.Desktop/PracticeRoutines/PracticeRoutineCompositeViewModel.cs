using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Selection;
using CygSoft.SmartSession.Desktop.Supports.Messages;
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
        private PracticeRoutineManagementViewModel practiceRoutineManagementViewModel;
        private PracticeRoutineEditViewModel practiceRoutineEditViewModel;
        private PracticeRoutineRecordingListViewModel practiceRoutineRecordingListViewModel;
        private ExerciseSelectionViewModel exerciseSelectionViewModel;
        private IPracticeRoutineService practiceRoutineService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public PracticeRoutineCompositeViewModel(IPracticeRoutineService practiceRoutineService, 
            PracticeRoutineManagementViewModel practiceRoutineManagementViewModel,
            PracticeRoutineRecordingListViewModel practiceRoutineRecordingListViewModel,
            PracticeRoutineEditViewModel practiceRoutineEditViewModel, ExerciseSelectionViewModel exerciseSelectionViewModel)
        {
            this.practiceRoutineService = practiceRoutineService;
            this.practiceRoutineManagementViewModel = practiceRoutineManagementViewModel;
            this.practiceRoutineEditViewModel = practiceRoutineEditViewModel;
            this.practiceRoutineRecordingListViewModel = practiceRoutineRecordingListViewModel;
            this.exerciseSelectionViewModel = exerciseSelectionViewModel;

            Messenger.Default.Register<ExitPracticeListMessage>(this, (m) => ExitPracticeList());
            Messenger.Default.Register<ViewPracticeListMessage>(this, (m) => ViewPracticeList(m.PracticeRoutineId));
            Messenger.Default.Register<ExerciseSelectionCancelledMessage>(this, (m) => ExerciseSelectionCancelled());
            Messenger.Default.Register<ExerciseSelectedMessage>(this, (m) => ExerciseSelected(m.ExerciseId));
            Messenger.Default.Register<StartSelectingPracticeRoutineExerciseMessage>(this, (m) => StartSelectingPracticeRoutine());
            Messenger.Default.Register<StartEditingPracticeRoutineMessage>(this, (m) => StartEditingPracticeRoutine(m.PracticeRoutine));
            Messenger.Default.Register<EndEditingPracticeRoutineMessage>(this, (m) => EndEditingPracticeRoutine(m.PracticeRoutine, m.Operation, m.LifeCycleState));

            NavigationCommand = new RelayCommand<string>(NavigateTo);
            NavigateTo("Search");
        }

        private void ExitPracticeList()
        {
            NavigateTo("Search");
        }

        private void ViewPracticeList(int practiceRoutineId)
        {
            practiceRoutineRecordingListViewModel.InitializeSession(practiceRoutineId);
            NavigateTo("Record");
        }

        private void ExerciseSelected(int exerciseId)
        {
            var timeSlotExercise = practiceRoutineService.CreateTimeSlotExercise(exerciseId);
            practiceRoutineEditViewModel.AddTimeSlotExercise(timeSlotExercise);

            //TODO: What do do about this? Is it actually still necessary?
            //var routineExercise = practiceRoutineService.CreatePracticeRoutineExerciseFor(exerciseId);
            //practiceRoutineEditViewModel.AddPracticeRoutineExercise(routineExercise);
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
                    CurrentViewModel = practiceRoutineManagementViewModel;
                    practiceRoutineManagementViewModel.RefreshRoutines();
                    break;
                case "Edit":
                    CurrentViewModel = practiceRoutineEditViewModel;
                    break;
                case "Record":
                    CurrentViewModel = practiceRoutineRecordingListViewModel;
                    break;
                case "SelectExercise":
                    CurrentViewModel = exerciseSelectionViewModel;
                    break;
                default:
                    CurrentViewModel = practiceRoutineManagementViewModel;
                    break;
            }
        }
    }
}
