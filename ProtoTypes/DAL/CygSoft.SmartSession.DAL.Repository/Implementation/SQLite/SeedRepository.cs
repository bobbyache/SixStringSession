﻿using CygSoft.SmartSession.DAL.Common;
using CygSoft.SmartSession.DAL.Repository.Implementation;

namespace CygSoft.SmartSession.DAL.Repository.SQLite
{
    public class SeedRepository : SQLiteContext
    {
        public void Goal()
        {
            var sql = @"
                INSERT INTO goal (name) VALUES 
                ('Happy Chappy'),
                ('Britannia Hotel'),
                ('Hollywood Bets');
            ";
            ExecuteNonQuery(sql);
        }

        public void Task()
        {
            var sql = @"
                INSERT INTO task (name) VALUES 
                ('Happy Chappy'),
                ('Britannia Hotel'),
                ('Hollywood Bets');
            ";
            ExecuteNonQuery(sql);
        }

        public void User()
        {
            var _password = new Hash().Go("password1"); //7C6A180B36896A0A8C02787EEAFB0E4C

            var sql = string.Format(@"
INSERT INTO user (has_access, cellphone, password, name, surname) VALUES 
(1, 0820000000, '{0}', 'Joe', 'Blogs'),
(1, 0830000000, '{0}', 'Sue', 'Blogs');",
_password);
            ExecuteNonQuery(sql);
        }

        public void Restaurant()
        {
            var sql = @"
INSERT INTO restaurant (name) VALUES 
('Happy Chappy'),
('Britannia Hotel'),
('Hollywood Bets');";
            ExecuteNonQuery(sql);
        }

        public void Score()
        {
            var sql = @"
INSERT INTO score (restaurant_id, user_id, insert_date, taste, temperature, tomorrow) VALUES 
(1, 1, CURRENT_TIMESTAMP, 5, 5, 5);";
            ExecuteNonQuery(sql);
        }
    }
}
