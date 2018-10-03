﻿using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.EF.Repositories;

namespace CygSoft.SmartSession.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartSessionContext context;

        public UnitOfWork(SmartSessionContext context)
        {
            this.context = context;
            Exercises = new ExerciseRepository(context);
            FileAttachments = new FileAttachmentRepository(context);
        }

        public IExerciseRepository Exercises { get; private set; }

        public IFileAttachmentRepository FileAttachments { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
