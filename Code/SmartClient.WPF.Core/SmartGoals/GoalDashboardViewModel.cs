using Caliburn.Micro;
using LiveCharts;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class GoalDashboardViewModel: BaseScreen
    {
        private readonly GoalManager goalManager;
        private IGoalDetail goal;

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

        public GoalDashboardViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, GoalManager goalManager) 
            : base(eventAggregator, dialogService, settingsService)
        {
            this.goalManager = goalManager;    
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            this.goal = this.goalManager.GetDetail();
            return base.OnActivateAsync(cancellationToken);
        }
    }
}
