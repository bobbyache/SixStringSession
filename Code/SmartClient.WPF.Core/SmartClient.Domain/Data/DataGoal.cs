using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SmartClient.Domain.Data
{
    public class DataGoal : IDataGoal
    {
        public static DataGoal Create()
        {
            return new DataGoal()
            {
                Id = Guid.NewGuid().ToString(),
                Tasks = new List<IDataGoalTask>(),
                Title = "New Goal",
                Weighting = 0.5
            };
        }

        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<IDataGoalTask> Tasks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Weighting { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
