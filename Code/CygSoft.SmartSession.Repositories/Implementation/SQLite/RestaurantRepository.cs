using System.Collections.Generic;
using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class RestaurantRepository : SQLiteContext, IRestaurantRepository
    {
        public int Insert(RestaurantRecord obj)
        {
            var sql = @"
INSERT INTO restaurant
(name)
VALUES
(@Name);
SELECT last_insert_rowid();";
            return Insert<RestaurantRecord>(
                sql,
                obj);
        }

        public RestaurantRecord Select(int id)
        {
            var sql = @"
SELECT * FROM restaurant 
WHERE id = @id;";
            return Select<RestaurantRecord>(
                sql,
                new { id });
        }

        public List<RestaurantRecord> SelectList()
        {
            var sql = @"
SELECT * FROM restaurant 
ORDER BY name";
            return SelectList<RestaurantRecord>(
                sql);
        }
    }
}
