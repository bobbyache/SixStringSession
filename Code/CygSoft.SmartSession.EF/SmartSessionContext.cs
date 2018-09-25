using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Keywords;
using Microsoft.EntityFrameworkCore;
using SmartSession.Domain.Records;

namespace CygSoft.SmartSession.EF
{
    public class SmartSessionContext : DbContext
    {
        private string connectionString;

        public DbSet<Goal> Goals { get; set; }
        public DbSet<PracticeTask> Tasks { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Keyword> Keywords { get; set; }


        //public DbSet<SessionPracticeTask> SessionTasks { get; set; }

        //public DbSet<SessionPracticeTask> TaskSessions { get; set; }

        public SmartSessionContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        // for dependency injection...
        public SmartSessionContext(string connectionString, DbContextOptions<SmartSessionContext> options) : base(options)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // or sql lite or sql ce
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // maps a many to many relationship...
            modelBuilder.Entity<GoalPracticeTask>()
                .HasKey(s => new { s.GoalId, s.TaskId });
            modelBuilder.Entity<SessionPracticeTask>()
                .HasKey(s => new { s.SessionId, s.PracticeTaskId });
            modelBuilder.Entity<ExerciseKeyword>()
                .HasKey(s => new { s.ExerciseId, s.KeywordId });
        }
    }
}
