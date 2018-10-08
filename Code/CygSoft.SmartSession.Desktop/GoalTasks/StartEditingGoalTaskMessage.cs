
namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    internal class StartEditingGoalTaskMessage
    {
        public GoalTaskSearchResultModel GoalTaskSearchResult { get; }

        public StartEditingGoalTaskMessage(GoalTaskSearchResultModel goalTaskSearchResult)
        {
            GoalTaskSearchResult = goalTaskSearchResult;
        }
    }
}