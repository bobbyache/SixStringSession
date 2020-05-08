using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SmartClient.Domain
{
    public class EditableGoalTask : IEditableGoalTask
    {
        private readonly IDataGoalTask dataGoalTask;

        public EditableGoalTask(string goalTitle, IDataGoalTask dataGoalTask)
        {
            if (string.IsNullOrEmpty(dataGoalTask.Id))
            {
                this.GoalTitle = goalTitle;
                this.dataGoalTask = dataGoalTask;
                this.dataGoalTask.Title = "New Task";
                this.dataGoalTask.Weighting = 0.5;
                this.dataGoalTask.Start = 0;
                this.dataGoalTask.Target = 100;
                this.dataGoalTask.Id = Guid.NewGuid().ToString();
            }
            else
            {
                this.GoalTitle = goalTitle;
                this.dataGoalTask = dataGoalTask;
            }            
        }

        public string GoalTitle { get; }
        public string Id => this.dataGoalTask.Id;
        public string Title
        {
            get => this.dataGoalTask.Title;
            set => this.dataGoalTask.Title = value;
        }

        public double Weighting
        {
            get => this.dataGoalTask.Weighting;
            set => this.dataGoalTask.Weighting = value;
        }
        public double Start 
        {
            get => this.dataGoalTask.Start;
            set => this.dataGoalTask.Start = value;
        }
        public double Target 
        {
            get => this.dataGoalTask.Target;
            set => this.dataGoalTask.Target = value;
        }
    }
}

