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
        private readonly GoalManager goalManager;

        public IList<IGoalTaskSummary> TaskSummaries { get { return this.goalManager.GetTaskSummaries(); } }
        private IEventAggregator eventAggregator { get; }

        public GoalDashboardViewModel(IEventAggregator eventAggregator, GoalManager goalManager)
        {
            // goalManager.Save(@"C:\Code\dummy_goal_saved.json");

            this.eventAggregator = eventAggregator;
            this.goalManager = goalManager;
            this.eventAggregator.SubscribeOnUIThread(this);
            goalManager.Open(@"C:\Code\dummy_goal.json");
        }
    }
}
