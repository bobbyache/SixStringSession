using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartGoals
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigateToMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IntroViewModel introViewModel;
        private readonly MainMenuViewModel mainMenuViewModel;
        private readonly ExampleViewModel exampleViewModel;
        private readonly GoalDashboardViewModel goalDashboardViewModel;
        private readonly TaskDashboardViewModel taskDashboardViewModel;

        public BottomMenuViewModel BottomMenuViewModel { get; }

        public ShellViewModel(IEventAggregator eventAggregator, 
            IntroViewModel introViewModel,
            MainMenuViewModel mainMenuViewModel, 
            ExampleViewModel exampleViewModel, 
            BottomMenuViewModel bottomMenuViewModel,
            GoalDashboardViewModel goalDashboardViewModel,
            TaskDashboardViewModel taskDashboardViewModel
            )
        {
            this.eventAggregator = eventAggregator;
            this.introViewModel = introViewModel;
            this.mainMenuViewModel = mainMenuViewModel;
            this.exampleViewModel = exampleViewModel;
            BottomMenuViewModel = bottomMenuViewModel;
            this.goalDashboardViewModel = goalDashboardViewModel;
            this.taskDashboardViewModel = taskDashboardViewModel;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            eventAggregator.SubscribeOnUIThread(this);
            // Will set this.ActiveItem for View
            return ActivateItemAsync(this.introViewModel, cancellationToken);
            // return base.OnActivateAsync(cancellationToken);
        }

        public override Task DeactivateItemAsync(Screen item, bool close, CancellationToken cancellationToken = default)
        {
            eventAggregator.Unsubscribe(this);
            return base.DeactivateItemAsync(item, close, cancellationToken);
        }

        public Task HandleAsync(NavigateToMessage message, CancellationToken cancellationToken)
        {
            if (message.NavigateTo == NavigateTo.Examples)
            {
                this.ActiveItem = exampleViewModel;
            }
            else if (message.NavigateTo == NavigateTo.GoalDashboard)
            {
                this.ActiveItem = goalDashboardViewModel;
            }
            else if (message.NavigateTo == NavigateTo.TaskDashboard)
            {
                this.ActiveItem = taskDashboardViewModel;
            }

            return Task.CompletedTask;
        }
    }
}