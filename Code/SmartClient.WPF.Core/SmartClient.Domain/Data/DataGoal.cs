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

        public string Id { get; set; }
        public string Title { get; set; }
        public IList<IDataGoalTask> Tasks { get; set; }
        public double Weighting { get; set; }
    }
}
