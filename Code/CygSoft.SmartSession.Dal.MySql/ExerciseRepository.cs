using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using Dapper;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class ExerciseRepository : RepositoryBase, IExerciseRepository
    {
        public ExerciseRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(Exercise entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertExercise", 
                param: new {
                    _title = entity.Title,
                    _difficultyRating = entity.DifficultyRating,
                    _practicalityRating = entity.PracticalityRating,
                    _percentageCompleteCalculationType = entity.PercentageCompleteCalculationType,
                    _initialMetronomeSpeed = entity.InitialMetronomeSpeed,
                    _targetMetronomeSpeed = entity.TargetMetronomeSpeed,
                    _targetPracticeTime = entity.TargetPracticeTime
                },
                commandType: CommandType.StoredProcedure
                );

            InsertNewExerciseActivities(entity);
        }

        public void AddRange(IEnumerable<Exercise> entities)
        {
            throw new System.NotImplementedException();
        }


        public IReadOnlyList<Exercise> Find(object criteria)
        {
            IExerciseSearchCriteria crit = (IExerciseSearchCriteria)criteria;

            var results = Connection.Query<Exercise>("sp_FindExercises",
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

        public Exercise Get(int id)
        {
            try
            {
                var result = Connection.QuerySingle<Exercise>("sp_GetExerciseById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);

                var activities = Connection.Query<ExerciseActivity>("sp_GetExerciseActivitiesByExercise",
                    param: new
                    {
                        _exerciseId = id
                    }, commandType: CommandType.StoredProcedure );

                result.ExerciseActivity = activities.ToList();

                return result;
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

            InsertNewExerciseActivities(entity);
        }

        private void InsertNewExerciseActivities(Exercise exercise)
        {
            foreach (ExerciseActivity activity in exercise.ExerciseActivity)
            {
                if (activity.Id == 0)
                {
                    Connection.ExecuteScalar<int>(sql: "sp_InsertExerciseActivity",
                    param: new
                    {
                        _exerciseId = exercise.Id,
                        _startTime = activity.StartTime,
                        _endTime = activity.EndTime,
                        _seconds = activity.Seconds,
                        _metronomeSpeed = activity.MetronomeSpeed
                    },
                    commandType: CommandType.StoredProcedure
                    );
                }
            }
        }
    }
}