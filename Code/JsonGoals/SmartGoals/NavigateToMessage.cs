namespace SmartGoals
{
    public enum NavigateTo
    {
        Home,
        Settings,
        GoalsMenu,
        GoalDashboard,
        TaskDashboard,
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
}