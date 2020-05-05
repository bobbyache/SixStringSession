using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class HeaderViewModel
    {
        private readonly IEventAggregator eventAggregator;

        public void NavigateToHome()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Home));
        }

        public void NavigateToSettings()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Settings));
        }

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
