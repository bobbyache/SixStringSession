using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using Dapper;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class PracticeRoutineRepository : RepositoryBase, IPracticeRoutineRepository
    {
        public PracticeRoutineRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(PracticeRoutine entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertPracticeRoutine",
                param: new
                {
                    _title = entity.Title
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public void AddRange(IEnumerable<PracticeRoutine> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<PracticeRoutine> Find(object criteria)
        {
            IPracticeRoutineSearchCriteria crit = (IPracticeRoutineSearchCriteria)criteria;

            var results = Connection.Query<PracticeRoutine>("sp_FindPracticeRoutines",
                param: new
                {
                    _title = crit.Title,
                    _fromDateCreated = crit.FromDateCreated,
                    _toDateCreated = crit.ToDateCreated,
                    _fromDateModified = crit.FromDateModified,
                    _toDateModified = crit.ToDateModified
                }, commandType: CommandType.StoredProcedure);

            return results.ToList();
        }

        public PracticeRoutine Get(int id)
        {
            try
            {
                var results = Connection.QuerySingle<PracticeRoutine>("sp_GetPracticeRoutineById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);

                return results;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for id: {id}", ex);
            }

        }

        public void Remove(PracticeRoutine entity)
        {
            var result = Connection.Execute("sp_DeletePracticeRoutine",
                param: new { _id = entity.Id }, commandType: CommandType.StoredProcedure);
        }

        public void RemoveRange(IEnumerable<PracticeRoutine> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PracticeRoutine entity)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdatePracticeRoutine",
                param: new
                {
                    _id = entity.Id,
                    _title = entity.Title
                },
                commandType: CommandType.StoredProcedure
                );
        }
    }
}