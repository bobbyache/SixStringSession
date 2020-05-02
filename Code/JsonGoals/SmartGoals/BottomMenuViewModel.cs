using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class BottomMenuViewModel: Screen
    {
        private readonly IEventAggregator eventAggregator;

        public void NavigateToExamples()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Examples));
        }

        public BottomMenuViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}
