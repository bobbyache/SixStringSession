using Caliburn.Micro;
using LiveCharts;
using SmartClient.Domain;
using SmartGoals.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class GoalDashboardViewModel: Screen
    {
        private readonly IDialogService dialogService;
        private readonly GoalManager goalManager;
        private IGoalDetail goal;
        private IEventAggregator eventAggregator { get; }

        public IList<IGoalTaskSummary> TaskSummaries { get { return this.goal.TaskSummaries; } }

        private IGoalTaskSummary selectedTask;
        public IGoalTaskSummary SelectedTaskSummary
        {
            get { return selectedTask; }
            set
            {
                this.selectedTask = value;
                NotifyOfPropertyChange("SelectedTaskSummary");
            }
        }

        public void OpenSelectedTask()
        {
            if (this.SelectedTaskSummary != null)
            {
                var task = this.SelectedTaskSummary;
                eventAggregator.PublishOnUIThreadAsync(new SelectGoalTaskDetailMessage(this.SelectedTaskSummary.Id));
                eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.TaskDashboard));
            }
        }

        public void AddTask()
        {

        }

        public void EditTask()
        {

        }

        public void DeleteTask()
        {

        }

        public void ViewTaskSummary()
        {

        }

        public string Title { get { return this.goal.Title; } }
        public int PercentProgress {  get { return this.goal.PercentProgress;  } }

        public GoalDashboardViewModel(IEventAggregator eventAggregator, IDialogService dialogService, GoalManager goalManager)
        {
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;
            this.goalManager = goalManager;
            this.eventAggregator.SubscribeOnUIThread(this);         
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            this.goal = this.goalManager.GetDetail();
            return base.OnActivateAsync(cancellationToken);
        }
    }
}
