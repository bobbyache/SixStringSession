using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb
{
    public class Task : IWeightedEntity
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("initial")] public double Initial { get; set; }

        [JsonPropertyName("target")] public double Target { get; set; }

        [JsonPropertyName("activity")] public IList<TaskActivity> Activity { get; set; } = new List<TaskActivity>();

        public int Weighting { get; set; } = 50;

        public double PercentCompleted()
        {
            var numerator = (double)(GetLatestValue() - Initial); 
            var denominator = (double)(Target - Initial);

            if (denominator == 0)
            {
                return 0;
            }
            else
            {
                var percentComplete = (numerator / denominator) * 100d;
                return percentComplete > 100 ? 100 : percentComplete;
            }
        }

        private double GetLatestValue()
        {
            if (!Activity.Any())
                return Initial;

            // get the last record, get the speed.
            var lastActivityDate = Activity.Max(a => a.Date);
            var manualProgress = Activity.Where(a => a.Date == lastActivityDate).Select(a => a.Value).Last();
            return manualProgress;
        }
    }


    public class TaskActivity
    {
        [JsonPropertyName("date")] public DateTime Date { get; set; }
        [JsonPropertyName("value")] public double Value { get; set; }
    }
}
