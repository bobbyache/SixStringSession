using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseSearchViewModel exerciseSearchViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(ExerciseSearchViewModel exerciseSearchViewModel, ExerciseEditViewModel exerciseEditViewModel)
        {
            this.exerciseSearchViewModel = exerciseSearchViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;

            Messenger.Default.Register<EditExerciseMessage>(this, (m) => OnNavigation("Edit"));
            Messenger.Default.Register<ExerciseEditedMessage>(this, (m) => OnNavigation("Search"));

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("Search");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = exerciseEditViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
