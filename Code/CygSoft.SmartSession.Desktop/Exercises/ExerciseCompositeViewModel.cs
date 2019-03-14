using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Desktop.Supports.Factories;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseManagementViewModel exerciseSearchViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;
        private IViewModelFactory viewModelFactory;
        private IExerciseService exerciseService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(
            IViewModelFactory viewModelFactory,
            IExerciseService exerciseService, 
            ExerciseManagementViewModel exerciseManagementViewModel, 
            ExerciseEditViewModel exerciseEditViewModel
            )
        {
            this.viewModelFactory = viewModelFactory;
            this.exerciseService = exerciseService;
            this.exerciseSearchViewModel = exerciseManagementViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;

            Messenger.Default.Register<StartEditingExerciseMessage>(this, (m) => StartEditingExercise(m.Exercise));
            Messenger.Default.Register<EndEditingExerciseMessage>(this, (m) => EndEditingExercise(m.Exercise, m.Operation, m.LifeCycleState));

            Messenger.Default.Register<CancelledExerciseRecordingMessage>(this, (m) => RecordingCancelled());
            Messenger.Default.Register<SavedExerciseRecordingMessage>(this, (m) => RecordingSaved());
            Messenger.Default.Register<StartPracticingExerciseMessage>(this, (m) => StartPracticingExercise(m.ExerciseId));

            NavigationCommand = new RelayCommand<string>((str) => CurrentViewModel = exerciseSearchViewModel);
            CurrentViewModel = exerciseSearchViewModel;
        }

        private void StartPracticingExercise(int exerciseId)
        {
            var exerciseRecorder = exerciseService.GetExerciseRecorder(exerciseId);
            var recorderViewModel = viewModelFactory.CreateRecorderViewModel(exerciseRecorder);
            CurrentViewModel = recorderViewModel;
        }

        private void RecordingCancelled()
        {
            CurrentViewModel = exerciseSearchViewModel;
        }

        private void RecordingSaved()
        {
            CurrentViewModel = exerciseSearchViewModel;
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
        }

        private void StartEditingExercise(IExercise exercise)
        {
            exerciseEditViewModel.StartEdit(exercise);
            CurrentViewModel = exerciseEditViewModel;
        }
    }
}
