using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using Dapper;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class ExerciseRepository : RepositoryBase, IExerciseRepository
    {
        public ExerciseRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(Exercise entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertExercise", 
                param: entity,
                commandType: CommandType.StoredProcedure
                );
        }

        public void AddRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            return Connection.Query<Exercise>("SELECT * FROM Exercise;").ToList();
        }

        public IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            return Connection.Query<Exercise>("SELECT * FROM Exercise;").ToList();
        }

        public IReadOnlyList<Exercise> Find(object criteria)
        {
            throw new System.NotImplementedException();
        }

        public Exercise Get(int id)
        {
            var results = Connection.QuerySingle<Exercise>("sp_GetExerciseById",
                param: new { _id = id }, commandType: CommandType.StoredProcedure);

            return results;
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