using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseManagementViewModel exerciseManagementViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;
        private ExerciseRecorderViewModel exerciseRecorderViewModel;
        private IExerciseService exerciseService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(IExerciseService exerciseService, ExerciseManagementViewModel exerciseManagementViewModel, 
            ExerciseEditViewModel exerciseEditViewModel,
            ExerciseRecorderViewModel exerciseRecorderViewModel)
        {
            this.exerciseService = exerciseService;
            this.exerciseManagementViewModel = exerciseManagementViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;
            this.exerciseRecorderViewModel = exerciseRecorderViewModel;

            Messenger.Default.Register<StartEditingExerciseMessage>(this, (m) => StartEditingExercise(m.Exercise));
            Messenger.Default.Register<EndEditingExerciseMessage>(this, (m) => EndEditingExercise(m.Exercise, m.Operation, m.LifeCycleState));
            Messenger.Default.Register<OpenExerciseRecorderMessage>(this, (m) => RecordExercise(m.ExerciseId));

            Messenger.Default.Register<CancelledExerciseRecordingMessage>(this, (m) => RecordingCancelled());
            Messenger.Default.Register<SavedExerciseRecordingMessage>(this, (m) => RecordingSaved());

            NavigationCommand = new RelayCommand<string>(NavigateTo);
            NavigateTo("Search");
        }

        private void RecordingCancelled()
        {
            NavigateTo("Search");
        }

        private void RecordingSaved()
        {
            NavigateTo("Search");
        }

        private void RecordExercise(int exerciseId)
        {
            exerciseRecorderViewModel.InitializeRecorder(exerciseId);
            NavigateTo("Record");
        }

        protected virtual void EndEditingExercise(IExercise exercise, EditorCloseOperation operation, 
            EntityLifeCycleState entityLifeCycleState)
        {
            if (operation == EditorCloseOperation.Saved)
            {
                if (entityLifeCycleState == EntityLifeCycleState.AsExistingEntity)
                    exerciseService.Update(exercise);
                else
                    exerciseService.Add(exercise);
            }

            NavigateTo("Search");
        }


        private void StartEditingExercise(IExercise exercise)
        {
            exerciseEditViewModel.StartEdit(exercise);
            NavigateTo("Edit");
        }

        private void NavigateTo(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = exerciseManagementViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = exerciseEditViewModel;
                    break;
                case "Record":
                    CurrentViewModel = exerciseRecorderViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseManagementViewModel;
                    break;
            }
        }
    }
}
