using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SmartClient.Domain
{
    public class GoalRepository : IGoalRepository
    {
        public virtual void Save(DataGoal goal, string filePath)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = false;

            File.WriteAllText(filePath, JsonSerializer.Serialize((DataGoal)goal, jsonSerializerOptions));
        }

        public virtual DataGoal Open(string filePath)
        {
            if (FileExists(filePath))
            {
                return Read(filePath);
            }
            else
            {
                return Create(filePath);
            }
        }

        protected virtual DataGoal Create(string filePath)
        {
            var dataGoal = DataGoal.Create();
            this.Save(dataGoal, filePath);

            return Read(filePath);
        }

        protected virtual DataGoal Read(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var goal = JsonSerializer.Deserialize<DataGoal>(jsonString);
            return goal;
        }

        protected virtual bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
