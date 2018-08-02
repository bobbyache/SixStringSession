using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class ScoreRepository : SQLiteContext, IScoreRepository
    {
        public int Insert(ScoreRecord obj)
        {
            var sql = @"
INSERT INTO score
(restaurant_id, user_id, insert_date, taste, temperature, tomorrow)
VALUES
(@RestaurantId, @UserId, @InsertDate, @Taste, @Temperature, @Tomorrow);
SELECT last_insert_rowid();";
            return Insert<ScoreRecord>(
                sql,
                obj);
        }

        public ScoreRecord Select(int id)
        {
            var sql = @"
SELECT * FROM score 
WHERE Id = @id;";
            return Select<ScoreRecord>(
                sql,
                new { id });
        }

        public List<ScoreRecord> SelectList()
        {
            var sql = @"
SELECT * FROM score 
ORDER BY restaurant_id;";
            return SelectList<ScoreRecord>(
                sql);
        }
    }
}
