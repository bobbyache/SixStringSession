using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Wpf;
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
                NotifyOfPropertyChange(() => ContentText);
            }
        }
        private IEventAggregator eventAggregator { get; }

        public ChartValues<double> Values1 { get; set; }
        public ChartValues<double> Values2 { get; set; }

        public ContentViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);

            Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            Values2 = new ChartValues<double> { 5, 3, 5, 7, 3, 9 };
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
