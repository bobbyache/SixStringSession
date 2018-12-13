using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using Dapper;
using KellermanSoftware.CompareNetObjects;

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
                    _targetMetronomeSpeed = entity.TargetMetronomeSpeed,
                    _speedProgressWeighting = entity.SpeedProgressWeighting,
                    _targetPracticeTime = entity.TargetPracticeTime,
                    _practiceTimeProgressWeighting = entity.PracticeTimeProgressWeighting
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
                        _speedProgressWeighting = entity.SpeedProgressWeighting,
                        _practiceTimeProgressWeighting = entity.PracticeTimeProgressWeighting
                },
                commandType: CommandType.StoredProcedure
                );

            DeleteMissingExerciseActivities(entity);
            InsertNewExerciseActivities(entity);
            UpdateChangedExerciseActivities(entity);
        }

        private void UpdateChangedExerciseActivities(Exercise exercise)
        {
            var persistedExercises = GetExerciseActivities(exercise);

            foreach (ExerciseActivity exerciseActivity in exercise.ExerciseActivity)
            {
                if (exerciseActivity.Id > 0)
                {
                    var persistedCounterPart = persistedExercises.Where(p => p.Id == exerciseActivity.Id).SingleOrDefault();
                    if (persistedCounterPart != null)
                    {
                        CompareLogic compareLogic = new CompareLogic();
                        ComparisonResult result = compareLogic.Compare(persistedCounterPart, exerciseActivity);
                        if (!result.AreEqual)
                        {
                            UpdateExerciseActivity(exerciseActivity);
                        }
                    }
                }
            }
        }

        public void UpdateExerciseActivity(ExerciseActivity exerciseActivity)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdateExerciseActivity",
                param: new
                {
                    _id = exerciseActivity.Id,
                    _startTime = exerciseActivity.StartTime,
                    _endTime = exerciseActivity.EndTime,
                    _seconds = exerciseActivity.Seconds,
                    _metronomeSpeed = exerciseActivity.MetronomeSpeed
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public ExerciseActivity GetExerciseActivity(int id)
        {
            try
            {
                var result = Connection.QuerySingle<ExerciseActivity>("sp_GetExerciseActivityById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for id: {id}", ex);
            }

        }

        private void InsertNewExerciseActivities(Exercise exercise)
        {
            foreach (ExerciseActivity activity in exercise.ExerciseActivity)
            {
                if (activity.Id <= 0)
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

        private IEnumerable<ExerciseActivity> GetExerciseActivities(Exercise entity)
        {
            var results = Connection.Query<ExerciseActivity>("sp_GetExerciseActivitiesByExercise",
            param: new
            {
                _exerciseId = entity.Id
            }, commandType: CommandType.StoredProcedure);

            return results;
        }

        private void DeleteMissingExerciseActivities(Exercise entity)
        {
            var results = GetExerciseActivities(entity);

            var missingIds = results
                    .Select(a => a.Id)
                    .Except(entity.ExerciseActivity.Select(a => a.Id));

            foreach (var id in missingIds)
            {
                Connection.Execute("sp_DeleteExerciseActivity",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}