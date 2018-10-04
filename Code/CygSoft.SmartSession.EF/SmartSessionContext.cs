using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.GoalTasks;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.DomainLegacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CygSoft.SmartSession.EF
{
    public class SmartSessionContext : DbContext
    {
        public static readonly LoggerFactory MyConsoleLoggerFactory = new LoggerFactory(
            new[]
            {
                new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
            });

        private string connectionString;

        public DbSet<CygSoft.SmartSession.Domain.Goals.Goal> Goals { get; set; }
        public DbSet<GoalTask> GoalTasks { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        public DbSet<PracticeSessionResult> PracticeSessionResults { get; set; }

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
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // maps a many to many relationship...
            modelBuilder.Entity<ExerciseKeyword>()
                .HasKey(s => new { s.ExerciseId, s.KeywordId });
            modelBuilder.Entity<FileAttachmentKeyword>()
                .HasKey(s => new { s.FileAttachmentId, s.KeywordId });
            modelBuilder.Entity<GoalKeyword>()
                .HasKey(s => new { s.GoalId, s.KeywordId });
            modelBuilder.Entity<GoalTaskKeyword>()
                .HasKey(s => new { s.GoalTaskId, s.KeywordId });
        }
    }
}
