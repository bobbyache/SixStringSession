using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;
namespace CygSoft.SmartSession.Repositories.SQLite{
    public class GoalRepository : SQLiteContext, IGoalRepository
    {
        public int Insert(GoalModel obj)
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

            return Insert<GoalModel>(sql, obj);
        }

        public GoalModel Select(int id)
        {
            var sql = @"
				SELECT * FROM goal 
				WHERE Id = @id;";

            return Select<GoalModel>(sql, new { id });
        }

        public List<GoalModel> SelectList()
        {
            var sql = @"
				SELECT * FROM goal 
				ORDER BY name;
				";

            return SelectList<GoalModel>(sql);
        }
    }
}
