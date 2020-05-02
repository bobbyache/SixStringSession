namespace SmartGoals
{
    public enum NavigateTo
    {
        Home,
        Settings,
        GoalsMenu,
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