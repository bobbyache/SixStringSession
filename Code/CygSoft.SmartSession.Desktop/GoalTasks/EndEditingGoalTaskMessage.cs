namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    internal class EndEditingGoalTaskMessage
    {
        public GoalTaskModel GoalTaskModel { get; }

        public EndEditingGoalTaskMessage(GoalTaskModel goalTaskModel)
        {
            GoalTaskModel = goalTaskModel;
        }
    }
}
