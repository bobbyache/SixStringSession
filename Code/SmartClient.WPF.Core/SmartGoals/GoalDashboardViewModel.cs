using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System.Collections.Generic;
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
                NotifyOfPropertyChange(() => SelectedTaskSummary);
            }
        }

        public void ViewTaskSummary()
        {
            if (this.SelectedTaskSummary != null)
            {
                Notify(new SelectGoalTaskDetailMessage(this.SelectedTaskSummary.Id));
                Notify(new NavigateToMessage(NavigateTo.TaskDashboard));
            }
        }

        public void AddTask()
        {
            Notify(new NavigateToMessage(NavigateTo.AddTask));
        }

        public void EditTask()
        {

        }

        public void DeleteTask()
        {
            if (selectedTask != null)
            {
                if (Dialogs.YesNoPrompt("Delete Task", $"Sure you'd like to delete task\n{selectedTask.Title}"))
                {
                    this.goalManager.DeleteTask(selectedTask.Id);
                    this.goalManager.Save();
                    this.goal = this.goalManager.GetDetail();
                    NotifyOfPropertyChange(() => TaskSummaries);
                    NotifyOfPropertyChange(() => PercentProgress);
                }
            }
        }

        public string Title { get { return this.goal.Title; } }
        public int PercentProgress {  get { return this.goal.PercentProgress;  } }

        public GoalDashboardViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, 
            GoalManager goalManager) 
            : base(eventAggregator, dialogService, settingsService)
        {
            this.goalManager = goalManager;    
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            this.goal = this.goalManager.GetDetail();

            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => PercentProgress);
            NotifyOfPropertyChange(() => TaskSummaries);
            NotifyOfPropertyChange(() => SelectedTaskSummary);

            return base.OnActivateAsync(cancellationToken);
        }
    }
}
