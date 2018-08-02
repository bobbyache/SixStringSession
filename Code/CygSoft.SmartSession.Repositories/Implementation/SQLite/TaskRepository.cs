using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;
namespace CygSoft.SmartSession.Repositories.SQLite{
    public class TaskRepository : SQLiteContext, ITaskRepository
    {
        public int Insert(TaskModel obj)
        {
            var sql = @"
				INSERT INTO task
				(
					name
				)
				VALUES
				(
					@Name
				);
				SELECT last_insert_rowid();
				";

            return Insert<TaskModel>(sql, obj);
        }

        public TaskModel Select(int id)
        {
            var sql = @"
				SELECT * FROM task 
				WHERE Id = @id;";

            return Select<TaskModel>(sql, new { id });
        }

        public List<TaskModel> SelectList()
        {
            var sql = @"
				SELECT * FROM task 
				ORDER BY name;
				";

            return SelectList<TaskModel>(sql);
        }
    }
}
