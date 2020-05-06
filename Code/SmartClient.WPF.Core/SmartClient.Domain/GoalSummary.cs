using System.Dynamic;

namespace SmartClient.Domain
{
    public class GoalSummary : IGoalSummary
    {
        public GoalSummary(string id, string title, int percentProgress)
        {
            this.Id = id;
            this.Title = title;
            this.PercentProgress = percentProgress;
        }
        public string Id { get; private set; }

        public string Title { get; set; }

        public int PercentProgress { get; private set; }
    }
}