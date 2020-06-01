using Caliburn.Micro;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;

namespace SmartGoals
{
    public class MainMenuViewModel: BaseScreen
    {
        public void NavigateToHome()
        {
            Notify(new NavigateToMessage(NavigateTo.Home));
        }

        public void NavigateToSettings()
        {
            // Notify(new NavigateToMessage(NavigateTo.Settings));
        }

        public void NavigateToGoalsMenu()
        {
            // Notify(new NavigateToMessage(NavigateTo.GoalManager));
        }

        public void NavigateToRoutinesMenu()
        {
            // Notify(new NavigateToMessage(NavigateTo.RoutineManager));
        }

        public void NavigateToExamples()
        {
            Notify(new NavigateToMessage(NavigateTo.Examples));
        }

        public MainMenuViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService)
            : base(eventAggregator, dialogService, settingsService)
        {
        }
    }
}
