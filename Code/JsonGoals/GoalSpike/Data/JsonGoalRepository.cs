using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb.Data
{
    public class JsonGoalRepository
    {
        private string folderPath;

        public IList<JsonGoalListItem> GetGoalList(string fileName)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, fileName));
            var goalList = JsonSerializer.Deserialize<List<JsonGoalListItem>>(jsonString);
            return goalList;
        }

        public void SaveGoalList(IList<JsonGoalListItem> goalList, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(goalList);
            File.WriteAllText(Path.Combine(this.folderPath, fileName), jsonString);
        }

        public void UpdateGoalList(IList<JsonGoalListItem> goalList)
        {
            foreach (var item in goalList)
            {
                UpdateGoalListItem(item);
            }
        }

        public void UpdateGoalListItem(JsonGoalListItem item)
        {
            var goal = GetGoalDocument(item.Id);
            item.Title = goal.Title;
            item.PercentComplete = goal.GetPercentComplete();
        }

        public JsonGoal GetGoalDocument(string id)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, $"{id}.json"));
            var goal = JsonSerializer.Deserialize<JsonGoal>(jsonString);
            return goal;
        }
        public JsonGoal GetGoalDocument(string filePath, int test)
        {
            var jsonString = File.ReadAllText(filePath);
            var goal = JsonSerializer.Deserialize<JsonGoal>(jsonString);
            return goal;
        }


        public JsonGoalRepository(string folderPath)
        {
            this.folderPath = folderPath;
        }
    }
}
