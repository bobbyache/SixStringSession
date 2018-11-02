using System.Collections.Generic;
using System.Data;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class ExerciseRepository : RepositoryBase, IExerciseRepository
    {
        private IDbTransaction _transaction;

        public ExerciseRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(Exercise entity)
        {
            throw new System.NotImplementedException();
        }

        public void AddRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            throw new System.NotImplementedException();
        }

        public Exercise Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Exercise entity)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Exercise entity)
        {
            throw new System.NotImplementedException();
        }
    }
}