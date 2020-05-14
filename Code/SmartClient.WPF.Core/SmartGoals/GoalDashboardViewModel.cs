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
        private IGoalDetail goal;

        public IList<IGoalTaskSummary> TaskSummaries { get { return this.goal.TaskSummaries; } }
        private IEventAggregator eventAggregator { get; }

        public string Title { get { return this.goal.Title; } }
        public int PercentProgress {  get { return this.goal.PercentProgress;  } }

        public GoalDashboardViewModel(IEventAggregator eventAggregator, GoalManager goalManager)
        {
            // goalManager.Save(@"C:\Code\dummy_goal_saved.json");
            
            
            this.eventAggregator = eventAggregator;
            this.goalManager = goalManager;
            this.eventAggregator.SubscribeOnUIThread(this);
            goalManager.Open(@"C:\Code\dummy_goal_2.json");
            this.goal = this.goalManager.GetDetail();
            
        }
    }
}
