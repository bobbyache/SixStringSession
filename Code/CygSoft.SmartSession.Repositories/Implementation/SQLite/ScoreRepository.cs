﻿using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class ScoreRepository : SQLiteContext, IScoreRepository
    {
        public int Insert(ScoreModel obj)
        {
            var sql = @"
INSERT INTO score
(restaurant_id, user_id, insert_date, taste, temperature, tomorrow)
VALUES
(@RestaurantId, @UserId, @InsertDate, @Taste, @Temperature, @Tomorrow);
SELECT last_insert_rowid();";
            return Insert<ScoreModel>(
                sql,
                obj);
        }

        public ScoreModel Select(int id)
        {
            var sql = @"
SELECT * FROM score 
WHERE Id = @id;";
            return Select<ScoreModel>(
                sql,
                new { id });
        }

        public List<ScoreModel> SelectList()
        {
            var sql = @"
SELECT * FROM score 
ORDER BY restaurant_id;";
            return SelectList<ScoreModel>(
                sql);
        }
    }
}
