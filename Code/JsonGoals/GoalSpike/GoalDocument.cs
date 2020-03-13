using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonDb
{
    public class GoalDocument
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("tasks")] public IList<Task> Tasks { get; set; } = new List<Task>();

        public double GetPercentComplete()
        {
            var calculator = new WeightedProgressCalculator();

            foreach (var task in Tasks)
            {
                calculator.Add(task);
            }
            return calculator.CalculateTotalProgress();
        }
    }
}
