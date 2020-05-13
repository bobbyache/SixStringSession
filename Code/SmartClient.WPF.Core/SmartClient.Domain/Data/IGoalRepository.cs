namespace SmartClient.Domain.Data
{
    public interface IGoalRepository
    {
        DataGoal Open(string filePath);
        void Save(DataGoal goal, string filePath);
    }
}