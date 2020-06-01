using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using SmartClient.Domain;
using System.Threading;
using System.Threading.Tasks;
using SmartGoals.Supports.CommonScreens;
using SmartGoals.Services;
using SmartGoals.Charts;

namespace SmartGoals
{
    public class TaskDashboardViewModel : BaseScreen, IHandle<SelectGoalTaskDetailMessage>
    {
        private IGoalTaskDetail goalTask;
        private readonly GoalManager goalManager;

        public string GoalTitle
        {
            get { return "Goal Title"; }
        }

        public string Title
        {
            get { return this.goalTask.Title; }
        }

        public double Start
        {
            get { return this.goalTask.Start; }
        }

        public double Target
        {
            get { return this.goalTask.Target; }
        }

        public int WeightingPercentage
        {
            get { return (int)(this.goalTask.Weighting * 100); }
        }

        public IList<IGoalTaskProgressSnapshot> HistoryItems {  get { return this.goalTask.TaskProgressSnapshots; } }

        public int ProgressValue { get { return this.goalTask.PercentProgress; } }

        public TaskDashboardViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, 
            GoalManager goalManager) 
            : base(eventAggregator, dialogService, settingsService)
        {
            this.goalManager = goalManager;     
        }

        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Series { get; set; }

        public Task HandleAsync(SelectGoalTaskDetailMessage message, CancellationToken cancellationToken)
        {
            this.goalTask = goalManager.GetTaskDetail(message.TaskId);
            
            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => WeightingPercentage);
            NotifyOfPropertyChange(() => Target);
            NotifyOfPropertyChange(() => Start);
            NotifyOfPropertyChange(() => ProgressValue);
            NotifyOfPropertyChange(() => HistoryItems);

            TaskProgressLinearChart linearChart = new TaskProgressLinearChart(goalTask.TaskProgressSnapshots);
            Series = linearChart.Series;
            Formatter = linearChart.Formatter;

            NotifyOfPropertyChange(() => Series);

            return base.OnActivateAsync(cancellationToken);
        }
    } 
}

