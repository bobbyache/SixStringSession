using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.Common
{
    public interface IConnectionManager
    {
        IDbConnection GetConnection();
    }
}
