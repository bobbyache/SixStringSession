using System;
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
            IExerciseSearchCriteria crit = (IExerciseSearchCriteria)criteria;

            var results = Connection.Query<Exercise>("sp_FindExercises",
                param: new
                {
                    _fromDateModified = crit.FromDateModified,
                    _toDateModified = crit.ToDateModified
                }, commandType: CommandType.StoredProcedure);

            return results.ToList();
        }

        public Exercise Get(int id)
        {
            try
            {
                var results = Connection.QuerySingle<Exercise>("sp_GetExerciseById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);

                return results;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for id: {id}", ex);
            }

        }

        public void Remove(Exercise entity)
        {
            var result = Connection.Execute("sp_DeleteExercise",
                param: new { _id = entity.Id }, commandType: CommandType.StoredProcedure);
        }

        public void RemoveRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Exercise entity)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdateExercise",
                param: new {
                        _id = entity.Id,
                    	_title = entity.Title, 
	                    _difficultyRating = entity.DifficultyRating,
	                    _practicalityRating = entity.PracticalityRating,
	                    _targetPracticeTime = entity.TargetPracticeTime,
	                    _targetMetronomeSpeed = entity.TargetMetronomeSpeed,
	                    _initialMetronomeSpeed = entity.InitialMetronomeSpeed
                },
                commandType: CommandType.StoredProcedure
                );
        }
    }
}