using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CygSoft.SmartSession.Desktop
{
    public class MainWindowViewModel : ViewModelBase
    {
        //private ExerciseSearchViewModel exerciseSearchViewModel;
        private TaskSearchViewModel taskSearchViewModel = new TaskSearchViewModel();
        private GoalSearchViewModel goalSearchViewModel = new GoalSearchViewModel();

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigiation);
        }

        private void OnNavigiation(string destination)
        {
            switch (destination)
            {
                case "TaskSearch":
                    CurrentViewModel = taskSearchViewModel;
                    break;
                case "GoalSearch":
                    CurrentViewModel = goalSearchViewModel;
                    break;
                default:
                    CurrentViewModel = taskSearchViewModel;
                    break;
            }
        }
    }
}
