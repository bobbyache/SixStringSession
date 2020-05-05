using SmartSession.Domain.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb.Data
{
    public class JsonGoalRepository : IGoalRepository
    {
        private string folderPath;

        IDataGoal IGoalRepository.GetGoalDocument(string filePath, int test)
        {
            var jsonString = File.ReadAllText(filePath);
            var goal = JsonSerializer.Deserialize<JsonGoal>(jsonString);
            return goal;
        }
    }
}
