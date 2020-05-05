using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CygSoft.SmartSession.Dal.MySql.Common
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly string connectionString;

        public ConnectionManager(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IDbConnection GetConnection()
        {
            var conn = new MySqlConnection(connectionString);
            return conn;
        }
    }
}
