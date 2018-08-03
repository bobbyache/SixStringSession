using CygSoft.SmartSession.Domain.Users;
using CygSoft.SmartSession.Repositories.Implementation;

namespace CygSoft.SmartSession.Repositories.SQLite
{
    public class UserRepository : SQLiteContext, IUserRepository
    {
        public int Insert(User obj)
        {
            var sql = @"
                INSERT INTO user
                (has_access, cellphone, password, name, surname)
                VALUES
                (@HasAccess, @CellPhone, @Password, @Name, @Surname);
                SELECT last_insert_rowid();";
            return Insert<User>(
                sql,
                obj);
        }

        public User Select(long cellPhone, string password)
        {
            var sql = @"
                SELECT * FROM user 
                WHERE cellphone = @cellPhone
                AND password = @password;";
            return Select<User>(
                sql,
                new { cellPhone, password });
        }
    }
}
