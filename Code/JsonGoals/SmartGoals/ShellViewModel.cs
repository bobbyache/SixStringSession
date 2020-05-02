using Caliburn.Micro;
using JsonDb;
using System;
using System.Threading;
using System.Windows;

namespace SmartGoals
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ExampleViewModel exampleViewModel;

        public ShellViewModel(IEventAggregator eventAggregator, ExampleViewModel exampleViewModel)
        {
            this.eventAggregator = eventAggregator;
            this.exampleViewModel = exampleViewModel;
        }

        protected override System.Threading.Tasks.Task OnActivateAsync(CancellationToken cancellationToken)
        {
            eventAggregator.SubscribeOnUIThread(this);
            // Will set this.ActiveItem for View
            return ActivateItemAsync(this.exampleViewModel, cancellationToken);
            // return base.OnActivateAsync(cancellationToken);
        }

        public override System.Threading.Tasks.Task DeactivateItemAsync(Screen item, bool close, CancellationToken cancellationToken = default)
        {
            eventAggregator.Unsubscribe(this);
            return base.DeactivateItemAsync(item, close, cancellationToken);
        }
    }
}