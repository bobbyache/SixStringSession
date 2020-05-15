using SmartClient.Domain.Common;
using SmartClient.Domain.Data;
using SmartClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SmartClient.Domain
{
    public class EditableGoalTask : IEditableGoalTask
    {
        private readonly DataGoalTask dataGoalTask;

        public EditableGoalTask(string goalTitle, DataGoalTask dataGoalTask)
        {
            if (string.IsNullOrEmpty(dataGoalTask.Id))
            {
                this.GoalTitle = goalTitle;
                this.dataGoalTask = dataGoalTask;
                this.dataGoalTask.Title = "New Task";
                this.dataGoalTask.UnitOfMeasure = "BPM";
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
            get { return this.dataGoalTask.Title; }
            set {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                    throw new InvalidTitleException("Invalid title");
                this.dataGoalTask.Title = value;
            }
        }

        public double Weighting
        {
            get { return this.dataGoalTask.Weighting; }
            set 
            {
                if (value < 0 || value > 1)

                    throw new InvalidWeightingException("Weighting must be between 0 and 1");
                this.dataGoalTask.Weighting = value; 
            }
        }

        public double Start
        {
            get { return this.dataGoalTask.Start; }
            set
            {
                if (this.dataGoalTask.Target <= value)
                {
                    throw new OutOfProgressHistoryBoundsException("Start value cannot equal or more than the Target");
                }

                if (this.dataGoalTask.ProgressHistory.Count > 0)
                {
                    var minValue = this.dataGoalTask.ProgressHistory.Select(s => s.Value).Min();
                    if (value > minValue)
                    {
                        throw new OutOfProgressHistoryBoundsException("Start value would place some progress history out of bounds");
                    }
                }
                this.dataGoalTask.Start = value;
            }
        }

        public double Target 
        {
            get { return this.dataGoalTask.Target; }
            set
            {
                if (this.dataGoalTask.Start >= value)
                {
                    throw new OutOfProgressHistoryBoundsException("Target value cannot be equal or less than the Start");
                }

                if (this.dataGoalTask.ProgressHistory.Count > 0)
                {
                    var maxValue = this.dataGoalTask.ProgressHistory.Select(s => s.Value).Max();
                    if (value < maxValue)
                    {
                        throw new OutOfProgressHistoryBoundsException("Target value would place some progress history out of bounds");
                    }
                }
                this.dataGoalTask.Target = value;
            }
        }

        public TaskUnitOfMeasure UnitOfMeasure 
        {
            get { return Utils.ParseToEnum<TaskUnitOfMeasure>(this.dataGoalTask.UnitOfMeasure); }
            set { this.dataGoalTask.UnitOfMeasure = value.ToString(); }
        }
    }
}

