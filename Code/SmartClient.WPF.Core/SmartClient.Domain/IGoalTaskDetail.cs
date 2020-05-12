namespace SmartClient.Domain
{
    public interface IGoalTaskDetail
    {
        string Id { get; }

        string Title { get; }

        public double Weighting { get; }

        public double Start { get; }
        public double Target { get; }

        public int PercentProgress { get; }

        IGoalTaskProgressSnapshot[] TaskProgressSnapshots { get; }
    }
}