
using CygSoft.SmartSession.BaseRepository.Implementation;

namespace CygSoft.SmartSession.BaseRepository.SQLite
{
    public class DropAndCreateRepository : SQLiteContext
    {

        public void Goal()
        {
            var sql = @"
			    DROP TABLE IF EXISTS goal; 
			    CREATE TABLE Goal (id INTEGER PRIMARY KEY, name VARCHAR(50))
			    ";
            ExecuteNonQuery(sql);
        }

        public void Task()
        {
          var sql = @"
            DROP TABLE IF EXISTS task; 
            CREATE TABLE Task (id INTEGER PRIMARY KEY, name VARCHAR(50))";
                  ExecuteNonQuery(sql);
        }

    public void User()
        {
            var sql = @"
DROP TABLE IF EXISTS user; 
CREATE TABLE user (id INTEGER PRIMARY KEY, has_access BOOL, cellphone LONG, password VARCHAR(32), name VARCHAR(50), surname VARCHAR(50))";
            ExecuteNonQuery(sql);

        }

        public void Restaurant()
        {
            var sql = @"
DROP TABLE IF EXISTS restaurant; 
CREATE TABLE restaurant (id INTEGER PRIMARY KEY, name VARCHAR(50))";
            ExecuteNonQuery(sql);
        }

        public void Score()
        {
            var sql = @"
DROP TABLE IF EXISTS score; 
CREATE TABLE score (id INTEGER PRIMARY KEY, restaurant_id INTEGER, user_id INTEGER, insert_date TIMESTAMP, taste INTEGER, temperature INTEGER, tomorrow INTEGER)";
            ExecuteNonQuery(sql);
        }
    }
}
