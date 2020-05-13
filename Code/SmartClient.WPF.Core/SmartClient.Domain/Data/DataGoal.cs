using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartClient.Domain.Data
{
    public class DataGoal
    {
        public static DataGoal Create()
        {
            return new DataGoal()
            {
                Id = Guid.NewGuid().ToString(),
                Tasks = new List<DataGoalTask>(),
                Title = "New Goal",
                Weighting = 0.5
            };
        }

        [JsonPropertyName("id")] public virtual string Id { get; set; }
        [JsonPropertyName("title")] public virtual string Title { get; set; }
        [JsonPropertyName("tasks")] public virtual IList<DataGoalTask> Tasks { get; set; }
        [JsonPropertyName("weighting")] public virtual double Weighting { get; set; }
    }
}
