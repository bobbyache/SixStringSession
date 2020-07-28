using SmartClient.Domain;

namespace SmartGoals
{
    public enum NavigateTo
    {
        Home,
        GoalDashboard,
        TaskDashboard,
        CreateGoal,
        AddTask
    }

    public sealed class NavigateToMessage
    {
        public NavigateToMessage(NavigateTo navigateTo)
        {
            NavigateTo = navigateTo;
        }

        public NavigateTo NavigateTo { get; }
    }

    public sealed class SelectGoalTaskDetailMessage
    {
        public string TaskId { get; private set; }
        public SelectGoalTaskDetailMessage(string taskId)
        {
            this.TaskId = taskId;
        }
    }

    public sealed class AddTaskMessage
    {
        public IGoalDetail Goal { get; private set; }
        public AddTaskMessage(IGoalDetail goal)
        {
            this.Goal = goal;
        }
    }
}