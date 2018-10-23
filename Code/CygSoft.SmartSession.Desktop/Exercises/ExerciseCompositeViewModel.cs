using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseSearchViewModel exerciseSearchViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;
        private ExerciseRecorderViewModel exerciseRecorderViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(ExerciseSearchViewModel exerciseSearchViewModel, 
            ExerciseEditViewModel exerciseEditViewModel,
            ExerciseRecorderViewModel exerciseRecorderViewModel)
        {
            this.exerciseSearchViewModel = exerciseSearchViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;
            this.exerciseRecorderViewModel = exerciseRecorderViewModel;

            Messenger.Default.Register<StartEditingExerciseMessage>(this, (m) => StartEditingExercise(m.ExerciseSearchResult));
            Messenger.Default.Register<EndEditingExerciseMessage>(this, (m) => EndEditingExercise(m.ExerciseModel));
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

        private void EndEditingExercise(ExerciseModel exerciseModel)
        {
            NavigateTo("Search");
        }


        private void StartEditingExercise(ExerciseSearchResultModel exerciseSearchResult)
        {
            exerciseEditViewModel.StartEdit(exerciseSearchResult);
            NavigateTo("Edit");
        }

        private void NavigateTo(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = exerciseEditViewModel;
                    break;
                case "Record":
                    CurrentViewModel = exerciseRecorderViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
