using CygSoft.SmartSession.GoalManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    internal class Goal
    {
        private List<IGoalFile> goalFiles = new List<IGoalFile>();
        private List<IGoalTask> goalTasks = new List<IGoalTask>();

        public Goal()
        {
            CreateDate = DateTime.Now;
        }

        internal Goal(IGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            if (goalTasks != null)
                this.goalTasks = new List<IGoalTask>(goalTasks);

            if (goalFiles != null)
                this.goalFiles = new List<IGoalFile>(goalFiles);
        }

        public double FileCount => goalFiles.Count();

        internal IGoalTask AddTask(string title, GoalTaskType goalTaskType)
        {
            //IGoalTask goalTask;

            if (goalTaskType == GoalTaskType.Duration)
            {
                DurationGoalTask goalTask = new DurationGoalTask(title);
                goalTasks.Add(goalTask);
                goalTask.Weighting = 100;
                return goalTask;
            }
            else if (goalTaskType == GoalTaskType.Percent)
            {
                PercentGoalTask goalTask = new PercentGoalTask(title);
                goalTasks.Add(goalTask);
                goalTask.Weighting = 100;
                return goalTask;
            }
            else if (goalTaskType == GoalTaskType.Metronome)
            {
                MetronomeGoalTask goalTask = new MetronomeGoalTask(title);
                goalTasks.Add(goalTask);
                goalTask.Weighting = 100;
                return goalTask;
            }
            else
                throw new NotImplementedException();
        }

        public IGoalFile[] Files => goalFiles.ToArray();
        public int MinutesPracticed { get; private set; }

        public DateTime CreateDate { get; private set; }

        public int Weighting => 0;

        public bool IsConsideredComplete => false;

        public int TaskCount
        {
            get
            {
                if (goalTasks != null)
                    return goalTasks.Count();
                return 0;
            }
        }

        public IGoalTask[] Tasks => goalTasks.ToArray();
    }
}
