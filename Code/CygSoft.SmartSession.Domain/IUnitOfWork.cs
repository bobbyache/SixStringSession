﻿using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.GoalTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IExerciseRepository Exercises { get; }
        IGoalRepository Goals { get; }
        IGoalTaskRepository GoalTasks { get; }
        IFileAttachmentRepository FileAttachments { get; }
        int Complete();
    }
}