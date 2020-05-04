using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb.Data
{
    public class JsonGoalTask : IWeightedEntity
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("start")] public double Start { get; set; }

        [JsonPropertyName("target")] public double Target { get; set; }

        [JsonPropertyName("history")] public IList<JsonGoalTaskHistoryItem> History { get; set; } = new List<JsonGoalTaskHistoryItem>();

        [JsonPropertyName("weighting")] public double Weighting { get; set; } = 0.5;

        public double PercentCompleted()
        {
            var numerator = (double)(GetLatestValue() - Start); 
            var denominator = (double)(Target - Start);

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
            if (!History.Any())
                return Start;

            // get the last record, get the speed.
            var lastActivityDate = History.Max(a => a.Date);
            var manualProgress = History.Where(a => a.Date == lastActivityDate).Select(a => a.Value).Last();
            return manualProgress;
        }
    }


    public class JsonGoalTaskHistoryItem
    {
        [JsonPropertyName("d")] public DateTime Date { get; set; }
        [JsonPropertyName("v")] public double Value { get; set; }
    }
}
