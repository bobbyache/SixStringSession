using System.Collections.Generic;
using CygSoft.SmartSession.BaseRepository.Implementation;
using CygSoft.SmartSession.BaseRepository.Interface;
using CygSoft.SmartSession.BaseRepository.Schema;

namespace CygSoft.SmartSession.BaseRepository.SQLite
{
    public class RestaurantRepository : SQLiteContext, IRestaurantRepository
    {
        public int Insert(RestaurantModel obj)
        {
            var sql = @"
INSERT INTO restaurant
(name)
VALUES
(@Name);
SELECT last_insert_rowid();";
            return Insert<RestaurantModel>(
                sql,
                obj);
        }

        public RestaurantModel Select(int id)
        {
            var sql = @"
SELECT * FROM restaurant 
WHERE id = @id;";
            return Select<RestaurantModel>(
                sql,
                new { id });
        }

        public List<RestaurantModel> SelectList()
        {
            var sql = @"
SELECT * FROM restaurant 
ORDER BY name";
            return SelectList<RestaurantModel>(
                sql);
        }
    }
}
