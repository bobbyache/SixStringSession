using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    public class GoalTaskCompositeViewModel : ViewModelBase
    {
        private GoalTaskSearchViewModel goalTaskSearchViewModel;
        private GoalTaskEditViewModel goalTaskEditViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public GoalTaskCompositeViewModel(GoalTaskSearchViewModel goalTaskSearchViewModel, GoalTaskEditViewModel goalTaskEditViewModel)
        {
            this.goalTaskSearchViewModel = goalTaskSearchViewModel;
            this.goalTaskEditViewModel = goalTaskEditViewModel;

            Messenger.Default.Register<StartEditingGoalTaskMessage>(this, (m) => StartEditingGoalTask(m.GoalTaskSearchResult));
            Messenger.Default.Register<EndEditingGoalTaskMessage>(this, (m) => EndEditingGoalTask(m.GoalTaskModel));

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("Search");
        }

        private void EndEditingGoalTask(GoalTaskModel goalTaskModel)
        {
            OnNavigation("Search");
        }


        private void StartEditingGoalTask(GoalTaskSearchResultModel goalTaskSearchResult)
        {
            goalTaskEditViewModel.StartEdit(goalTaskSearchResult);
            OnNavigation("Edit");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = goalTaskSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = goalTaskEditViewModel;
                    break;
                default:
                    CurrentViewModel = goalTaskSearchViewModel;
                    break;
            }
        }
    }
}