using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class MainMenuViewModel: Screen
    {
        private readonly IEventAggregator eventAggregator;

        public void NavigateToHome()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Home));
        }

        public void NavigateToSettings()
        {
            // eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Settings));
        }

        public void NavigateToGoalsMenu()
        {
            // eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.GoalManager));
        }

        public void NavigateToRoutinesMenu()
        {
            // eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.RoutineManager));
        }

        public void NavigateToExamples()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Examples));
        }

        public MainMenuViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
