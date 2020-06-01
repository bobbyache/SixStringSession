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
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Examples));
        }

        public void NavigateToGoal()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.GoalDashboard));
        }

        public void NavigateToHome()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Home));
        }

        public BottomMenuViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService) 
            : base(eventAggregator, dialogService, settingsService)
        {
        }
    }
}
