namespace SmartClient.Domain.Data
{
    public interface IGoalRepository
    {
        IDataGoal Open(string filePath);
        string Save(IDataGoal goal, string filePath);
    }
}