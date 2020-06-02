using Caliburn.Micro;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;

namespace SmartGoals
{
    public class BottomMenuViewModel: BaseScreen
    {
        public void NavigateToHome()
        {
            Notify(new NavigateToMessage(NavigateTo.Home));
        }

        public BottomMenuViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService) 
            : base(eventAggregator, dialogService, settingsService)
        {
        }
    }
}
