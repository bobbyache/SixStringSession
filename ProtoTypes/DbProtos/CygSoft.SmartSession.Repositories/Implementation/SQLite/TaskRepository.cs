using System.Collections.Generic;
using CygSoft.SmartSession.Domain.Tasks;
using CygSoft.SmartSession.Repositories.Implementation;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class TaskRepository : SQLiteContext, ITaskRepository
    {
        private class TaskRecord
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
        }

        private const string Metronome = "M";
        private const string Duration = "D";
        private const string Percent = "P";
        private const string Aggregate = "A";

        public int Insert(BaseTask obj)
        {
            var sql = @"
				INSERT INTO task
				(
					title,
                    type
				)
				VALUES
				(
					@Title,
                    @Type
				);
				SELECT last_insert_rowid();
				";

            var taskRec = new TaskRecord
            {
                Id = obj.Id,
                Title = obj.Title,
            };

            if (obj is MetronomeGoalTask)
                taskRec.Type = Metronome;
            if (obj is DurationGoalTask)
                taskRec.Type = Duration;
            if (obj is PercentGoalTask)
                taskRec.Type = Percent;
            if (obj is AggregateTask)
                taskRec.Type = Aggregate;

            return Insert<TaskRecord>(sql, taskRec);
        }

        public BaseTask Select(int id)
        {
            var sql = @"
				SELECT * FROM task 
				WHERE Id = @id;";

            var taskRecord = Select<TaskRecord>(sql, new { id });
            return GetTaskFromRecord(taskRecord);
        }

        public List<BaseTask> SelectList()
        {
            var sql = @"
				SELECT * FROM task 
				ORDER BY title;
				";

            return SelectList<BaseTask>(sql);
        }

        private BaseTask GetTaskFromRecord(TaskRecord taskRecord)
        {
            BaseTask task = null;

            if (taskRecord.Type == Metronome)
            {
                task = new MetronomeGoalTask();
                task.Id = taskRecord.Id;
                task.Title = taskRecord.Title;
            }
            else if (taskRecord.Type == Duration)
            {
                task = new DurationGoalTask();
                task.Id = taskRecord.Id;
                task.Title = taskRecord.Title;
            }
            else if (taskRecord.Type == Percent)
            {
                task = new PercentGoalTask();
                task.Id = taskRecord.Id;
                task.Title = taskRecord.Title;
            }
            else if (taskRecord.Type == Aggregate)
            {
                // https://www.c-sharpcorner.com/UploadFile/c5c6e2/populate-a-treeview-dynamically/
                // https://github.com/vectors36/dynamic_treeview_sqlite_winform
                task = new AggregateTask();
                task.Id = taskRecord.Id;
                task.Title = taskRecord.Title;
            }
            return task;
        }
    }
}
