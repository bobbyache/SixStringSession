using Microsoft.EntityFrameworkCore;
using SmartSession.Domain.Records;

namespace CygSoft.SmartSession.EF
{
    public class SmartSessionContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; }
        public DbSet<PracticeTask> Tasks { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }


        //public DbSet<SessionPracticeTask> SessionTasks { get; set; }

        //public DbSet<SessionPracticeTask> TaskSessions { get; set; }

        public SmartSessionContext()
        {

        }
        // for dependency injection...
        public SmartSessionContext(DbContextOptions<SmartSessionContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // or sql lite or sql ce
            optionsBuilder.UseSqlServer(@"server=ROBB-LT02\ROBLT;database=SmartSession_EF_2;Integrated Security=True;Connection Reset=true;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // maps a many to many relationship...
            modelBuilder.Entity<GoalPracticeTask>()
                .HasKey(s => new { s.GoalId, s.TaskId });
            modelBuilder.Entity<SessionPracticeTask>()
                .HasKey(s => new { s.SessionId, s.PracticeTaskId });
        }
    }
}
