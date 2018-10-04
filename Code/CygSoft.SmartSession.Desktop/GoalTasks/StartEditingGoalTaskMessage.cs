
namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    internal class StartEditingGoalTaskMessage
    {
        public GoalTaskSearchResult GoalTaskSearchResult { get; }

        public StartEditingGoalTaskMessage(GoalTaskSearchResult goalTaskSearchResult)
        {
            GoalTaskSearchResult = goalTaskSearchResult;
        }
    }
}