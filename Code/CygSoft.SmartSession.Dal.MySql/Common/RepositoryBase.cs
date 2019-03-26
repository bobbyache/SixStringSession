using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.Common
{
    public abstract class RepositoryBase
    {
        protected IDbConnection Connection { get; private set; }

        public RepositoryBase(IDbConnection connection)
        {
            Connection = connection;
        }
    }
}
