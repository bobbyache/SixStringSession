using CygSoft.SmartSession.Repositories.Implementation;

namespace CygSoft.SmartSession.BaseRepository.SQLite
{
    public class DropAndCreateRepository : SQLiteContext
    {

        public void Goal()
        {
            var sql = @"
			    DROP TABLE IF EXISTS goal; 
			    CREATE TABLE Goal (id INTEGER PRIMARY KEY, title VARCHAR(50))
			    ";
            ExecuteNonQuery(sql);
        }

        public void Task()
        {
          var sql = @"
            DROP TABLE IF EXISTS task; 
            CREATE TABLE Task (id INTEGER PRIMARY KEY, title VARCHAR(50), type VARCHAR(1))";
                  ExecuteNonQuery(sql);
        }

        public void User()
        {
            var sql = @"
                DROP TABLE IF EXISTS user; 
                CREATE TABLE user (id INTEGER PRIMARY KEY, has_access BOOL, cellphone LONG, password VARCHAR(32), name VARCHAR(50), surname VARCHAR(50))";
            ExecuteNonQuery(sql);

        }
    }
}
