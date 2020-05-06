namespace SmartClient.Domain
{
    public class GoalSummary : IGoalSummary
    {
        public GoalSummary(string id, string title)
        {
            this.Id = id;
            this.Title = title;
        }
        public string Id { get; private set; }

        public string Title { get; set; }

        public int PercentProgress => throw new System.NotImplementedException();
    }
}