namespace CygSoft.SmartSession.Desktop.Goals
{
    internal class EndEditingGoalMessage
    {
        public GoalModel GoalModel { get; }

        public EndEditingGoalMessage(GoalModel goalModel)
        {
            GoalModel = goalModel;
        }
    }
}