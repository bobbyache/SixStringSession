namespace SmartClient.Domain.Data
{
    public interface IGoalRepository
    {
        IDataGoal Open(string filePath);
        void Save(IDataGoal goal, string filePath);
    }
}