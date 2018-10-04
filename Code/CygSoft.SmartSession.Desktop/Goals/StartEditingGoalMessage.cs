namespace CygSoft.SmartSession.Desktop.Goals
{
    internal class StartEditingGoalMessage
    {
        public GoalSearchResult GoalSearchResult { get; }

        public StartEditingGoalMessage(GoalSearchResult goalSearchResult)
        {
            GoalSearchResult = goalSearchResult;
        }
    }
}