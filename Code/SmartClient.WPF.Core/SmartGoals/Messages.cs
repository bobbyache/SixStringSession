namespace SmartGoals
{
    public enum NavigateTo
    {
        Home,
        Settings,
        GoalsMenu,
        GoalDashboard,
        TaskDashboard,
        CreateGoal,
        RoutinesMenu,
        SelectPracticeRoutine,
        Examples
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
}