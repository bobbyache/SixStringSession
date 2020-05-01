namespace SmartGoals
{
    public enum NavigateTo
    {
        Home,
        Settings
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