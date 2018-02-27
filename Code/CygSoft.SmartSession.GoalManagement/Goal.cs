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

        public Goal() { }

        internal Goal(int minutesRecorded, IGoalTask[] goalTasks, IGoalFile[] goalFiles)
        {
            MinutesRecorded = minutesRecorded;
            this.goalTasks = goalTasks;
            this.goalFiles = goalFiles;
        }

        public double FileCount => goalFiles.Count();

        public IGoalFile[] Files => goalFiles;
        public int MinutesRecorded { get; internal set; }
        public double TaskCount => goalTasks.Count();
        public IGoalTask[] Tasks => goalTasks;
    }
}
