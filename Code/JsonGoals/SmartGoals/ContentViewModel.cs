using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class ContentViewModel: PropertyChangedBase, IHandle<NavigateToHomeMessage>, IHandle<NavigateToSettingsMessage>
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

        public Task HandleAsync(NavigateToHomeMessage message, CancellationToken cancellationToken)
        {
            this.ContentText = "Home";
            return Task.CompletedTask;
        }

        public Task HandleAsync(NavigateToSettingsMessage message, CancellationToken cancellationToken)
        {
            this.ContentText = "Settings";
            return Task.CompletedTask;
        }
    }
}
