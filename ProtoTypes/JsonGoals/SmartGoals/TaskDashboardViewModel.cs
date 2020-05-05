using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;
using SmartSession.Domain;

namespace SmartGoals
{
    public class TaskDashboardViewModel : Screen
    {
        private readonly GoalDocument goalDocument;

        private readonly GoalTask goalTask;

        private IEventAggregator eventAggregator { get; }

        public string GoalTitle
        {
            get { return this.goalDocument.Title; }
        }

        public string Title
        {
            get { return this.goalTask.Title; }
            set 
            {
                this.goalTask.Title = value;
                NotifyOfPropertyChange("title");
            }
        }

        public double Start
        {
            get { return this.goalTask.Start; }
            set { this.goalTask.Target = value; }
        }

        public double Target
        {
            get { return this.goalTask.Target; }
            set { this.goalTask.Target = value; }
        }

        public double Weighting
        {
            get { return this.goalTask.Weighting * 100; }
            set { this.goalTask.Weighting = value / 100; }
        }

        public IList<GoalTaskHistoryItem> HistoryItems {  get { return this.goalTask.History; } }


        public int ProgressValue { get { return 0; /* return  (int)Math.Round(this.goalTask.PercentCompleted()); */ } }

        
        public TaskDashboardViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);

            // goalDocument = goalRepository.GetGoalDocument(@"C:\Users\RobB\OneDrive - Korbitec Inc\Documents\Guitar\Goals\goal.json", 0);
            // this.goalTask = goalDocument.Tasks[0];


            // Example From: https://lvcharts.net/App/examples/v1/wpf/Date%20Time
            var dayConfig = Mappers.Xy<GoalTaskHistoryItem>()
              .X(dateModel => dateModel.Date.Ticks / TimeSpan.FromDays(1).Ticks)
              .Y(dateModel => dateModel.Value);

            //Notice you can also configure this type globally, so you don't need to configure every
            //SeriesCollection instance using the type.
            //more info at http://lvcharts.net/App/Index#/examples/v1/wpf/Types%20and%20Configuration
            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries { Values = new ChartValues<GoalTaskHistoryItem>(goalTask.History) },

            };

            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("d");
        }

        public Func<double, string> Formatter { get; set; }
        public SeriesCollection Series { get; set; }

    }
}

