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

        internal void AddTask(GoalTask goalTask)
        {
            if (goalTasks.Count == 0)
                goalTask.Weighting = 100;

            goalTasks.Add(goalTask);
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
