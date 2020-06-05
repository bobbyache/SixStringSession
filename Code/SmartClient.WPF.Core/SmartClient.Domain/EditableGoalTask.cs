using SmartClient.Domain.Common;
using SmartClient.Domain.Data;
using SmartClient.Domain.Exceptions;
using System;
using System.Linq;

namespace SmartClient.Domain
{
    public class EditableGoalTask : IEditableGoalTask
    {
        internal readonly DataGoalTask DataGoalTask;

        public EditableGoalTask(string goalTitle, DataGoalTask dataGoalTask)
        {
            if (string.IsNullOrEmpty(dataGoalTask.Id))
            {
                this.GoalTitle = goalTitle;
                this.DataGoalTask = dataGoalTask;
                this.DataGoalTask.Title = "New Task";
                this.DataGoalTask.UnitOfMeasure = "BPM";
                this.DataGoalTask.Weighting = 0.5;
                this.DataGoalTask.Start = 0;
                this.DataGoalTask.Target = 100;
                this.DataGoalTask.Id = Guid.NewGuid().ToString();
            }
            else
            {
                this.GoalTitle = goalTitle;
                this.DataGoalTask = dataGoalTask;
            }            
        }

        public string GoalTitle { get; }
        public string Id => this.DataGoalTask.Id;

        public string Title
        {
            get { return this.DataGoalTask.Title; }
            set {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                    throw new InvalidTitleException("Invalid title");
                this.DataGoalTask.Title = value;
            }
        }

        public double Weighting
        {
            get { return this.DataGoalTask.Weighting; }
            set 
            {
                if (value < 0 || value > 1)

                    throw new InvalidWeightingException("Weighting must be between 0 and 1");
                this.DataGoalTask.Weighting = value; 
            }
        }

        public double Start
        {
            get { return this.DataGoalTask.Start; }
            set
            {
                if (this.DataGoalTask.Target <= value)
                {
                    throw new OutOfProgressHistoryBoundsException("Start value cannot equal or more than the Target");
                }

                if (this.DataGoalTask.ProgressHistory.Count > 0)
                {
                    var minValue = this.DataGoalTask.ProgressHistory.Select(s => s.Value).Min();
                    if (value > minValue)
                    {
                        throw new OutOfProgressHistoryBoundsException("Start value would place some progress history out of bounds");
                    }
                }
                this.DataGoalTask.Start = value;
            }
        }

        public double Target 
        {
            get { return this.DataGoalTask.Target; }
            set
            {
                if (this.DataGoalTask.Start >= value)
                {
                    throw new OutOfProgressHistoryBoundsException("Target value cannot be equal or less than the Start");
                }

                if (this.DataGoalTask.ProgressHistory.Count > 0)
                {
                    var maxValue = this.DataGoalTask.ProgressHistory.Select(s => s.Value).Max();
                    if (value < maxValue)
                    {
                        throw new OutOfProgressHistoryBoundsException("Target value would place some progress history out of bounds");
                    }
                }
                this.DataGoalTask.Target = value;
            }
        }

        public TaskUnitOfMeasure UnitOfMeasure 
        {
            get { return Utils.ParseToEnum<TaskUnitOfMeasure>(this.DataGoalTask.UnitOfMeasure); }
            set { this.DataGoalTask.UnitOfMeasure = value.ToString(); }
        }
    }
}

