using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class GoalDashboardViewModel: Screen
    {
        public ChartValues<double> Values1 { get; set; }
        public ChartValues<double> Values2 { get; set; }

        private IEventAggregator eventAggregator { get; }

        public GoalDashboardViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);

            // GoalDocument goalDocument = goalRepository.GetGoalDocument(@"C:\Users\RobB\OneDrive - Korbitec Inc\Documents\Guitar\Goals\goal.json", 0);

            Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            Values2 = new ChartValues<double> { 5, 3, 5, 7, 3, 9 };

        }
    }
}
