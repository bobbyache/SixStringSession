using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb.Data
{
    public class JsonGoalTask
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("start")] public double Start { get; set; }

        [JsonPropertyName("target")] public double Target { get; set; }

        [JsonPropertyName("history")] public IList<JsonGoalTaskHistoryItem> History { get; set; } = new List<JsonGoalTaskHistoryItem>();

        [JsonPropertyName("weighting")] public double Weighting { get; set; } = 0.5;
    }


    public class JsonGoalTaskHistoryItem
    {
        [JsonPropertyName("d")] public DateTime Date { get; set; }
        [JsonPropertyName("v")] public double Value { get; set; }
    }
}
