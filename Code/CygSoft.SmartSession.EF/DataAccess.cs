using Microsoft.EntityFrameworkCore;
using SmartSession.Domain.Records;
using System;
using System.Linq;

namespace CygSoft.SmartSession.EF
{
    public class DataAccess
    {
        public void InsertGoal(Goal goal)
        {
            if (goal.Id > 0)
                throw new ArgumentException("A new goal cannot have an id");

            using (var context = new SmartSessionContext())
            {
                context.Goals.Add(goal);
                // context.Add(goal); // for polymorphic inserts.
                // context is now tracking the goal.
                context.SaveChanges(); // now is saved.
            }
        }

        public void InsertExercise(Exercise exercise)
        {
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");

            using (var context = new SmartSessionContext())
            {
                context.Exercises.Add(exercise);
                context.SaveChanges();
            }
        }

        public void InsertTask(PracticeTask task)
        {
            if (task.Id > 0)
                throw new ArgumentException("A new task cannot have an id");

            using (var context = new SmartSessionContext())
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public void InsertSession(Session session)
        {
            if (session.Id > 0)
                throw new ArgumentException("A new session cannot have an id");

            using (var context = new SmartSessionContext())
            {
                context.Sessions.Add(session);
                context.SaveChanges();
            }
        }

        public PracticeTask GetTask(int taskId)
        {
            if (taskId <= 0)
                throw new ArgumentException();

            using (var context = new SmartSessionContext())
            {
                var session = context.Tasks
                    .Include(s => s.TaskSessions)
                    .Where(s => s.Id == taskId)
                    .SingleOrDefault();
                return session;
            }
        }

        public Session GetSession(int sessionId)
        {
            if (sessionId <= 0)
                throw new ArgumentException();

            using (var context = new SmartSessionContext())
            {
                var session = context.Sessions
                    .Include(s => s.SessionTasks)
                        .ThenInclude(task => task.Metronome)
                    //.Include(s => s.)
                        //.ThenInclude(task => task.)
                        
                    //.Include(s => s.)
                    //.ThenInclude(t => t.id)
                    .Where(s => s.Id == sessionId)
                    .SingleOrDefault();
                return session;
            }
        }
    }
}
