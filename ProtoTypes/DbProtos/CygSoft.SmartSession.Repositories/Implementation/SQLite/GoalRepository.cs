using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Repositories.Implementation;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class GoalRepository : SQLiteContext, IGoalRepository
    {
        public int Insert(Goal obj)
        {
            var sql = @"
				INSERT INTO goal
				(
					title
				)
				VALUES
				(
					@Title
				);
				SELECT last_insert_rowid();
				";

            return Insert<Goal>(sql, obj);
        }

        public Goal Select(int id)
        {
            var sql = @"
				SELECT * FROM goal 
				WHERE Id = @id;";

            return Select<Goal>(sql, new { id });
        }

        public List<Goal> SelectList()
        {
            var sql = @"
				SELECT * FROM goal 
				ORDER BY title;
				";

            return SelectList<Goal>(sql);
        }
    }
}
