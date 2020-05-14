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
        private IEventAggregator eventAggregator { get; }

        public IList<IGoalTaskSummary> TaskSummaries { get { return this.goal.TaskSummaries; } }

        private IGoalTaskSummary selectedTask;
        public IGoalTaskSummary SelectedTaskSummary
        {
            get { return selectedTask; }
            set
            {
                this.selectedTask = value;
                NotifyOfPropertyChange("SelectedTask");
            }
        }

        public void OpenSelectedTask()
        {
            if (this.SelectedTaskSummary != null)
            {
                var task = this.SelectedTaskSummary;
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

        public GoalDashboardViewModel(IEventAggregator eventAggregator, GoalManager goalManager)
        {
            this.eventAggregator = eventAggregator;
            this.goalManager = goalManager;
            this.eventAggregator.SubscribeOnUIThread(this);
            goalManager.Open(@"C:\Code\dummy_goal_2.json");
            // goalManager.Save(@"C:\Code\dummy_goal_saved.json");
            this.goal = this.goalManager.GetDetail();
            
        }
    }
}
