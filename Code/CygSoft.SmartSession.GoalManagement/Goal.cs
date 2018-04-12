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
        private IGoalFile[] goalFiles;
        private IGoalTask[] goalTasks;

        public Goal()
        {
            CreateDate = DateTime.Now;
        }

        internal Goal(int minutesRecorded, IGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            MinutesPracticed = minutesRecorded;
            this.goalTasks = goalTasks;
            this.goalFiles = goalFiles;
        }

        public double FileCount => goalFiles.Count();

        public IGoalFile[] Files => goalFiles;
        public int MinutesPracticed { get; internal set; }

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

        public IGoalTask[] Tasks => goalTasks;
    }
}
