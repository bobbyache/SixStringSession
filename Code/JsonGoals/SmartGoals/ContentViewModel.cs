using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class ContentViewModel: PropertyChangedBase, IHandle<NavigateToMessage>
    {
        private string contentText = "Nothing Here";

        public string ContentText
        {
            get => contentText;
            set
            {
                this.contentText = value;
                NotifyOfPropertyChange("ContentText");
            }
        }
        private IEventAggregator eventAggregator { get; }

        public ContentViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);
        }

        public Task HandleAsync(NavigateToMessage message, CancellationToken cancellationToken)
        {
            if (message.NavigateTo == NavigateTo.Home)
            {
                this.ContentText = "Home";
            }
            else if (message.NavigateTo == NavigateTo.Settings)
            {
                this.ContentText = "Settings";
            }
            return Task.CompletedTask;
        }
    }
}
