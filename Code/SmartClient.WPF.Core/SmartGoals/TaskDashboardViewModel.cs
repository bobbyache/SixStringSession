using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using SmartClient.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class TaskDashboardViewModel : Screen, IHandle<SelectGoalTaskDetailMessage>
    {
        private IGoalTaskDetail goalTask;
        private readonly GoalManager goalManager;

        private IEventAggregator eventAggregator { get; }

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

        public TaskDashboardViewModel(IEventAggregator eventAggregator, GoalManager goalManager)
        {
            this.eventAggregator = eventAggregator;
            this.goalManager = goalManager;
            this.eventAggregator.SubscribeOnUIThread(this);            
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

            //// Example From: https://lvcharts.net/App/examples/v1/wpf/Date%20Time
            var dayConfig = Mappers.Xy<IGoalTaskProgressSnapshot>()
              .X(dateModel => dateModel.Day.Ticks / TimeSpan.FromDays(1).Ticks)
              .Y(dateModel => dateModel.Value);

            //Notice you can also configure this type globally, so you don't need to configure every
            //SeriesCollection instance using the type.
            //more info at http://lvcharts.net/App/Index#/examples/v1/wpf/Types%20and%20Configuration
            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries { Values = new ChartValues<IGoalTaskProgressSnapshot>(goalTask.TaskProgressSnapshots) }
            };

            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("d");

            NotifyOfPropertyChange(() => Series);

            return base.OnActivateAsync(cancellationToken);
        }
    } 
}

