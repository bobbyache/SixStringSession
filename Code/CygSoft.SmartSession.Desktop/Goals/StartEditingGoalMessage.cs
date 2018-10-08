namespace CygSoft.SmartSession.Desktop.Goals
{
    internal class StartEditingGoalMessage
    {
        public GoalSearchResultModel GoalSearchResult { get; }

        public StartEditingGoalMessage(GoalSearchResultModel goalSearchResult)
        {
            GoalSearchResult = goalSearchResult;
        }
    }
}