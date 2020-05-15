using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartClient.Domain.Data
{
    public class DataGoalTask
    {
        [JsonPropertyName("id")] public virtual string Id { get; set; }
        [JsonPropertyName("title")] public virtual string Title { get; set; }
        [JsonPropertyName("history")] public virtual IList<DataGoalTaskProgressSnapshot> ProgressHistory { get; set; } = new List<DataGoalTaskProgressSnapshot>();
        [JsonPropertyName("type")] public virtual string UnitOfMeasure { get; set; }
        [JsonPropertyName("start")] public virtual double Start { get; set; }
        [JsonPropertyName("target")] public virtual double Target { get; set; }
        [JsonPropertyName("weighting")] public virtual double Weighting { get; set; }
    }
}
