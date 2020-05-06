using System.Dynamic;

namespace SmartClient.Domain
{
    public class GoalTaskSummary : IGoalTaskSummary
    {
        public GoalTaskSummary(string id, string title, string goalTitle, int percentDone)
        {
            this.GoalTitle = goalTitle;
            this.Id = id;
            this.Title = title;
            this.PercentProgress = percentDone;
        }

        public string GoalTitle { get; private set; }

        public string Id { get; private set; }

        public string Title { get; private set; }

        public int PercentProgress { get; private set; }
    }
}