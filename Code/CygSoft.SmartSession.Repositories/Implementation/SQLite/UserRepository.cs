using CygSoft.SmartSession.Repositories.Implementation;
using CygSoft.SmartSession.Repositories.Interface;
using CygSoft.SmartSession.Repositories.Schema;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class UserRepository : SQLiteContext, IUserRepository
    {
        public int Insert(UserRecord obj)
        {
            var sql = @"
INSERT INTO user
(has_access, cellphone, password, name, surname)
VALUES
(@HasAccess, @CellPhone, @Password, @Name, @Surname);
SELECT last_insert_rowid();";
            return Insert<UserRecord>(
                sql,
                obj);
        }

        public UserRecord Select(long cellPhone, string password)
        {
            var sql = @"
SELECT * FROM user 
WHERE cellphone = @cellPhone
AND password = @password;";
            return Select<UserRecord>(
                sql,
                new { cellPhone, password });
        }
    }
}
