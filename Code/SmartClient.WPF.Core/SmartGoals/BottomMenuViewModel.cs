using Caliburn.Micro;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class BottomMenuViewModel: BaseScreen
    {
        public void NavigateToExamples()
        {
            Notify(new NavigateToMessage(NavigateTo.Examples));
        }

        public void NavigateToGoal()
        {
            Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
        }

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
