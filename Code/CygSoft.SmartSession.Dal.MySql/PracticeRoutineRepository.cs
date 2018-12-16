using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class PracticeRoutineRepository : RepositoryBase, IPracticeRoutineRepository
    {
        public PracticeRoutineRepository(IDbTransaction transaction) : base(transaction) { }
    }
}
