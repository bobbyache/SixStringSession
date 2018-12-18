using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CygSoft.SmartSession.Desktop
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ExerciseCompositeViewModel exerciseSearchViewModel;
        private GoalCompositeViewModel goalSearchViewModel;
        private PracticeRoutineCompositeViewModel practiceRoutineSearchViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public MainWindowViewModel(
            ExerciseCompositeViewModel exerciseCompositeViewModel, 
            GoalCompositeViewModel goalCompositeViewModel,
            PracticeRoutineCompositeViewModel practiceRoutineSearchViewModel)
        {
            this.exerciseSearchViewModel = exerciseCompositeViewModel;
            this.goalSearchViewModel = goalCompositeViewModel;
            this.practiceRoutineSearchViewModel = practiceRoutineSearchViewModel;

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
                case "PracticeRoutineSearch":
                    CurrentViewModel = practiceRoutineSearchViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
