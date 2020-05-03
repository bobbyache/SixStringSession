﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonDb
{
    public class GoalRepository
    {
        private string folderPath;

        public IList<GoalListItem> GetGoalList(string fileName)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, fileName));
            var goalList = JsonSerializer.Deserialize<List<GoalListItem>>(jsonString);
            return goalList;
        }

        public void SaveGoalList(IList<GoalListItem> goalList, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(goalList);
            File.WriteAllText(Path.Combine(this.folderPath, fileName), jsonString);
        }

        public void UpdateGoalList(IList<GoalListItem> goalList)
        {
            foreach (var item in goalList)
            {
                UpdateGoalListItem(item);
            }
        }

        public void UpdateGoalListItem(GoalListItem item)
        {
            var goal = GetGoalDocument(item.Id);
            item.Title = goal.Title;
            item.PercentComplete = goal.GetPercentComplete();
        }

        public GoalDocument GetGoalDocument(string id)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, $"{id}.json"));
            var goal = JsonSerializer.Deserialize<GoalDocument>(jsonString);
            return goal;
        }
        public GoalDocument GetGoalDocument(string filePath, int test)
        {
            var jsonString = File.ReadAllText(filePath);
            var goal = JsonSerializer.Deserialize<GoalDocument>(jsonString);
            return goal;
        }


        public GoalRepository(string folderPath)
        {
            this.folderPath = folderPath;
        }
    }
}
