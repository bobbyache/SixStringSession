using SmartSession.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonDb.Data
{
    public class JsonGoal: IDataGoal
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("tasks")] public IList<JsonGoalTask> Tasks { get; set; } = new List<JsonGoalTask>();

        [JsonPropertyName("weighting")] public double Weighting { get; set; } = 0.5;
    }
}
