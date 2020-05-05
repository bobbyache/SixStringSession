using System.Collections.Generic;

namespace SmartSession.Domain.Data
{
    public interface IGoalRepository
    {
        IDataGoal GetGoalDocument(string filePath, int test);
    }
}