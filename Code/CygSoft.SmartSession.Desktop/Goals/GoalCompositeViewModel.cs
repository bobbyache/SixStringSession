using CygSoft.SmartSession.Desktop.Supports.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Goals
{
    public class GoalCompositeViewModel : ViewModelBase
    {
        private GoalSearchViewModel goalSearchViewModel;
        private GoalEditViewModel goalEditViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public GoalCompositeViewModel(GoalSearchViewModel goalSearchViewModel, GoalEditViewModel goalEditViewModel)
        {
            this.goalSearchViewModel = goalSearchViewModel;
            this.goalEditViewModel = goalEditViewModel;

            Messenger.Default.Register<StartEditingGoalMessage>(this, (m) => StartEditingGoal(m.GoalSearchResult));
            Messenger.Default.Register<EndEditingGoalMessage>(this, (m) => EndEditingGoal(m.GoalModel));

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("Search");
        }

        private void EndEditingGoal(GoalModel goalModel)
        {
            OnNavigation("Search");
        }


        private void StartEditingGoal(GoalSearchResultModel goalSearchResult)
        {
            goalEditViewModel.StartEdit(goalSearchResult);
            OnNavigation("Edit");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = goalSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = goalEditViewModel;
                    break;
                default:
                    CurrentViewModel = goalSearchViewModel;
                    break;
            }
        }
    }
}