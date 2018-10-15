using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CygSoft.SmartSession.Desktop
{
    // Multiple Views and View Models: Opening Dialogs.... https://www.youtube.com/watch?v=Dzv8CtUCchY

    public class MainWindowViewModel : ViewModelBase
    {
        private ExerciseCompositeViewModel exerciseSearchViewModel;
        private GoalCompositeViewModel goalSearchViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public MainWindowViewModel(ExerciseCompositeViewModel exerciseSearchViewModel, 
            GoalCompositeViewModel goalSearchViewModel)
        {
            this.exerciseSearchViewModel = exerciseSearchViewModel;
            this.goalSearchViewModel = goalSearchViewModel;

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("ExerciseSearch");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "GoalSearch":
                    CurrentViewModel = goalSearchViewModel;
                    break;
                case "ExerciseSearch":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
