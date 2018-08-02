using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class GoalRepository : SQLiteContext, IGoalRepository
    {
        public int Insert(GoalRecord obj)
        {
            var sql = @"
				INSERT INTO goal
				(
					name
				)
				VALUES
				(
					@Name
				);
				SELECT last_insert_rowid();
				";

            return Insert<GoalRecord>(sql, obj);
        }

        public GoalRecord Select(int id)
        {
            var sql = @"
				SELECT * FROM goal 
				WHERE Id = @id;";

            return Select<GoalRecord>(sql, new { id });
        }

        public List<GoalRecord> SelectList()
        {
            var sql = @"
				SELECT * FROM goal 
				ORDER BY name;
				";

            return SelectList<GoalRecord>(sql);
        }
    }
}
