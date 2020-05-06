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

        public int Progress => throw new System.NotImplementedException();

        public int PercentDone => throw new System.NotImplementedException();
    }
}