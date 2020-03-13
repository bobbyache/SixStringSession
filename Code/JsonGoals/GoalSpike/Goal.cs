using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonDb
{
    public class Goal
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("tasks")] public IList<TaskReference> TaskReferences { get; set; }
    }

    public class TaskReference
    {
        [JsonPropertyName("id")] public string Id { get; set; }
    }
}
