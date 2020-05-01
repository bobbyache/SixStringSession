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
            eventAggregator.PublishOnUIThreadAsync(new NavigateToHomeMessage());
        }

        public void NavigateToSettings()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToSettingsMessage());
        }

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
