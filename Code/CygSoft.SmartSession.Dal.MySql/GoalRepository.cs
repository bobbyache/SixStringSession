using System.Collections.Generic;
using System.Data;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Goals;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class GoalRepository : RepositoryBase, IGoalRepository
    {
        private IDbTransaction _transaction;

        public GoalRepository(IDbTransaction transaction) : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Goal entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(IEnumerable<Goal> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Goal> Find(Specification<Goal> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Goal> Find(Specification<Goal> specification, int page = 0, int pageSize = 100)
        {
            throw new System.NotImplementedException();
        }

        public Goal Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Goal entity)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Goal> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Goal entity)
        {
            throw new System.NotImplementedException();
        }
    }
}