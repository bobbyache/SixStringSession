using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartClient.Domain.Data
{
    public class DataGoalTaskProgressSnapshot
    {
        [JsonPropertyName("d")] public virtual string Day { get; set; }
        [JsonPropertyName("v")] public virtual double Value { get; set; }
    }
}
