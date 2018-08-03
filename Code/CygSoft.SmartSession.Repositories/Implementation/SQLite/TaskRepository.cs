using System.Collections.Generic;
using CygSoft.SmartSession.Domain.Tasks;
using CygSoft.SmartSession.Repositories.Implementation;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class TaskRepository : SQLiteContext, ITaskRepository
    {
        public int Insert(GoalTaskRecord obj)
        {
            var sql = @"
				INSERT INTO task
				(
					title
				)
				VALUES
				(
					@Title
				);
				SELECT last_insert_rowid();
				";

            return Insert<GoalTaskRecord>(sql, obj);
        }

        public GoalTaskRecord Select(int id)
        {
            var sql = @"
				SELECT * FROM task 
				WHERE Id = @id;";

            return Select<GoalTaskRecord>(sql, new { id });
        }

        public List<GoalTaskRecord> SelectList()
        {
            var sql = @"
				SELECT * FROM task 
				ORDER BY title;
				";

            return SelectList<GoalTaskRecord>(sql);
        }
    }
}
