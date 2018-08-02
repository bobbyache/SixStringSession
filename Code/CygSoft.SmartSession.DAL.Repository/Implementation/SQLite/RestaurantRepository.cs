using System.Collections.Generic;
using CygSoft.SmartSession.DAL.Repository.Implementation;
using CygSoft.SmartSession.DAL.Repository.Interface;
using CygSoft.SmartSession.DAL.Repository.Schema;

namespace CygSoft.SmartSession.DAL.Repository.SQLite
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
