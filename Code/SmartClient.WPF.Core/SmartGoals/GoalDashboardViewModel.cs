using Caliburn.Micro;
using LiveCharts;
using SmartClient.Domain;
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

        public GoalDashboardViewModel(IEventAggregator eventAggregator, GoalManager goalManager)
        {
            goalManager.Open(@"C:\Code\dummy_goal.json");

            goalManager.Save(@"C:\Code\dummy_goal_saved.json");
            // var goalTask = goalManager.CreateTask();
            //goalTask.Title = "First Task";
            //goalTask.Start = 20;
            //goalTask.Target = 150;

            //goalManager.UpdateTaskProgressSnapshot(goalTask.Id, DateTime.Parse("2020-03-02"), 5);
            //goalManager.UpdateTaskProgressSnapshot(goalTask.Id, DateTime.Parse("2020-03-03"), 6);
            //goalManager.UpdateTaskProgressSnapshot(goalTask.Id, DateTime.Parse("2020-03-03"), 7);


            // goalManager.Save();

            this.eventAggregator = eventAggregator;
            this.eventAggregator.SubscribeOnUIThread(this);

            // GoalDocument goalDocument = goalRepository.GetGoalDocument(@"C:\Users\RobB\OneDrive - Korbitec Inc\Documents\Guitar\Goals\goal.json", 0);

            Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            Values2 = new ChartValues<double> { 5, 3, 5, 7, 3, 9 };

        }
    }
}
