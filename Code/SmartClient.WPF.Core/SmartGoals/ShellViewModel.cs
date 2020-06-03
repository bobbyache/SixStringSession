using Caliburn.Micro;
using SmartGoals.Supports.CommonScreens;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class ShellViewModel : Conductor<BaseScreen>.Collection.OneActive, IHandle<NavigateToMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IntroViewModel introViewModel;
        private readonly CreateGoalViewModel createGoalViewModel;
        private readonly GoalDashboardViewModel goalDashboardViewModel;
        private readonly TaskDashboardViewModel taskDashboardViewModel;
        private readonly AddTaskViewModel addTaskViewModel;

        public BottomMenuViewModel BottomMenuViewModel { get; }

        public ShellViewModel(IEventAggregator eventAggregator,
            IntroViewModel introViewModel,
            CreateGoalViewModel createGoalViewModel,
            BottomMenuViewModel bottomMenuViewModel,
            GoalDashboardViewModel goalDashboardViewModel,
            TaskDashboardViewModel taskDashboardViewModel,
            AddTaskViewModel addTaskViewModel
            )
        {
            this.eventAggregator = eventAggregator;
            this.introViewModel = introViewModel;
            this.createGoalViewModel = createGoalViewModel;
            this.BottomMenuViewModel = bottomMenuViewModel;
            this.goalDashboardViewModel = goalDashboardViewModel;
            this.taskDashboardViewModel = taskDashboardViewModel;
            this.addTaskViewModel = addTaskViewModel;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            eventAggregator.SubscribeOnUIThread(this);
            // Will set this.ActiveItem for View
            return ActivateItemAsync(this.introViewModel, cancellationToken);
        }

        public override Task DeactivateItemAsync(BaseScreen item, bool close, CancellationToken cancellationToken = default)
        {
            eventAggregator.Unsubscribe(this);
            return base.DeactivateItemAsync(item, close, cancellationToken);
        }

        public Task HandleAsync(NavigateToMessage message, CancellationToken cancellationToken)
        {
            if (message.NavigateTo == NavigateTo.Home)
            {
                this.ActiveItem = this.introViewModel;
            }
            else if (message.NavigateTo == NavigateTo.CreateGoal)
            {
                this.ActiveItem = createGoalViewModel;
            }
            else if (message.NavigateTo == NavigateTo.GoalDashboard)
            {
                this.ActiveItem = goalDashboardViewModel;
            }
            else if (message.NavigateTo == NavigateTo.TaskDashboard)
            {
                this.ActiveItem = taskDashboardViewModel;
            }
            else if (message.NavigateTo == NavigateTo.AddTask)
            {
                this.ActiveItem = addTaskViewModel;
            }

            return Task.CompletedTask;
        }
    }
}