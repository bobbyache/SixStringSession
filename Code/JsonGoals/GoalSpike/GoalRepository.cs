using System;
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

        public GoalRepository(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public Task GetTask(string id)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, $"T-{id}.json"));
            var task = JsonSerializer.Deserialize<Task>(jsonString);
            return task;
        }

        public Goal GetGoal(string id)
        {
            var jsonString = File.ReadAllText(Path.Combine(this.folderPath, $"G-{id}.json"));
            var goal = JsonSerializer.Deserialize<Goal>(jsonString);
            return goal;
        }

        public void SaveTasks(IList<Task> tasks)
        {
            foreach (var task in tasks)
            {
                SaveTask(task);
            }
        }

        public List<Task> GetTasks(IList<TaskReference> taskReferences)
        {
            return GetTasks(taskReferences.Select(t => t.Id).ToArray());
        }

        public List<Task> GetTasks(string[] taskIds)
        {
            var tasks = new List<Task>();

            foreach (var taskId in taskIds)
            {
                var task = GetTask(taskId);
                tasks.Add(task);
            }

            return tasks;
        }

        public void SaveTask(Task task)
        {
            var jsonString = JsonSerializer.Serialize(task);
            File.WriteAllText(Path.Combine(this.folderPath, $"T-{task.Id}.json"), jsonString);
        }
    }
}
