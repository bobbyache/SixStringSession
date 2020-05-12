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
        public virtual void Save(IDataGoal goal, string filePath)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = false;

            File.WriteAllText(filePath, JsonSerializer.Serialize((DataGoal)goal, jsonSerializerOptions));
        }

        public virtual IDataGoal Open(string filePath)
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

        protected virtual IDataGoal Create(string filePath)
        {
            var dataGoal = DataGoal.Create();
            this.Save(dataGoal, filePath);

            return Read(filePath);
        }

        protected virtual IDataGoal Read(string filePath)
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
