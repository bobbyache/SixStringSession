using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using Dapper;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CygSoft.SmartSession.Dal.MySql
{
    internal class ExerciseRepository : RepositoryBase, IExerciseRepository
    {
        public ExerciseRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(IExercise entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertExercise", 
                param: new {
                    _title = entity.Title,
                    _targetMetronomeSpeed = entity.TargetMetronomeSpeed,
                    _speedProgressWeighting = entity.SpeedProgressWeighting,
                    _targetPracticeTime = entity.TargetPracticeTime,
                    _practiceTimeProgressWeighting = entity.PracticeTimeProgressWeighting,
                    _manualProgressWeighting = entity.ManualProgressWeighting
                },
                commandType: CommandType.StoredProcedure
                );

            InsertNewExerciseActivities(entity);

            var persistedEntity = Get(entity.Id);
            entity.DateCreated = persistedEntity.DateCreated;
        }

        public void AddRange(IEnumerable<IExercise> entities)
        {
            throw new System.NotImplementedException();
        }


        public IReadOnlyList<IExercise> Find(object criteria)
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

        public IReadOnlyList<IExercise> GetPracticeRoutineExercises(int practiceRoutineId)
        {
            var exercises = Connection.Query<Exercise>("sp_GetPracticeRoutineExercises",
                param: new
                {
                    _practiceRoutineId = practiceRoutineId
                }, commandType: CommandType.StoredProcedure);

            string delimitedIds = string.Join(",", exercises.Select(ex => ex.Id));

            var activities = Connection.Query<ExerciseActivity>("sp_GetExerciseActivityByIds",
            param: new
            {
                _ids = delimitedIds
            }, commandType: CommandType.StoredProcedure);


            foreach (var exercise in exercises)
            {
                exercise.ExerciseActivity = activities.Where(a => a.ExerciseId == exercise.Id).ToList();
            }

            return exercises.ToList();
        }

        public IExercise Get(int id)
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

        public void Remove(IExercise entity)
        {
            var result = Connection.Execute("sp_DeleteExercise",
                param: new { _id = entity.Id }, commandType: CommandType.StoredProcedure);
        }

        public void RemoveRange(IEnumerable<IExercise> entities)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IExercise entity)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdateExercise",
                param: new {
                        _id = entity.Id,
                    	_title = entity.Title, 
	                    _targetPracticeTime = entity.TargetPracticeTime,
	                    _targetMetronomeSpeed = entity.TargetMetronomeSpeed,
                        _speedProgressWeighting = entity.SpeedProgressWeighting,
                        _practiceTimeProgressWeighting = entity.PracticeTimeProgressWeighting,
                        _manualProgressWeighting = entity.ManualProgressWeighting
                },
                commandType: CommandType.StoredProcedure
                );

            DeleteMissingExerciseActivities(entity);
            InsertNewExerciseActivities(entity);
            UpdateChangedExerciseActivities(entity);
        }

        public void Update(IEnumerable<IExercise> exercises)
        {
            foreach (var exercise in exercises)
            {
                Update(exercise);
            }
        }

        private void UpdateChangedExerciseActivities(IExercise exercise)
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
                    _seconds = exerciseActivity.Seconds,
                    _metronomeSpeed = exerciseActivity.MetronomeSpeed,
                    _manualProgress = exerciseActivity.ManualProgress
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

        private void InsertNewExerciseActivities(IExercise exercise)
        {
            foreach (ExerciseActivity activity in exercise.ExerciseActivity)
            {
                if (activity.Id <= 0)
                {
                    Connection.ExecuteScalar<int>(sql: "sp_InsertExerciseActivity",
                    param: new
                    {
                        _exerciseId = exercise.Id,
                        _seconds = activity.Seconds,
                        _metronomeSpeed = activity.MetronomeSpeed,
                        _manualProgress = activity.ManualProgress
                    },
                    commandType: CommandType.StoredProcedure
                    );
                }
            }
        }

        private IEnumerable<ExerciseActivity> GetExerciseActivities(IExercise entity)
        {
            var results = Connection.Query<ExerciseActivity>("sp_GetExerciseActivitiesByExercise",
            param: new
            {
                _exerciseId = entity.Id
            }, commandType: CommandType.StoredProcedure);

            return results;
        }

        private void DeleteMissingExerciseActivities(IExercise entity)
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