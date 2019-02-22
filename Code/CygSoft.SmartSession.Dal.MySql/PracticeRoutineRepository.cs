using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CygSoft.SmartSession.Dal.MySql.PracticeRoutines;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Domain.Sessions;
using Dapper;
using KellermanSoftware.CompareNetObjects;

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

            InsertNewRoutineExercises(entity);

            var persistedEntity = Get(entity.Id);
            entity.DateCreated = persistedEntity.DateCreated;
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
                var result = Connection.QuerySingle<PracticeRoutine>("sp_GetPracticeRoutineById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);

                var routineExercises = Connection.Query<PracticeRoutineExercise>("sp_GetPracticeRoutineExercisesByPracticeRoutine",
                param: new
                {
                    _practiceRoutineId = id
                }, commandType: CommandType.StoredProcedure);

                result.PracticeRoutineExercises = routineExercises.ToList();

                return result;
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
            throw new NotImplementedException();
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

            InsertNewRoutineExercises(entity);
            DeleteMissingRoutineExercises(entity);
            UpdateChangedPracticeRoutineExercises(entity);

            var persistedEntity = Get(entity.Id);
            entity.DateCreated = persistedEntity.DateCreated;
        }

        public PracticeRoutineRecorder GetPracticeRoutineRecorder(int id)
        {
            try
            {
                PracticeRoutine practiceRoutine = Get(id);

                var exerciseRecorderRecords = Connection.Query<PracticeRoutineExerciseRecorderRecord>("sp_GetPracticeRoutineExerciseRecordersByRoutineId",
                param: new
                {
                    _practiceRoutineId = id
                }, commandType: CommandType.StoredProcedure);

                List<ExerciseRecorder> exerciseRecorders = new List<ExerciseRecorder>();

                foreach (var rec in exerciseRecorderRecords)
                {
                    var speedProgress = new SpeedProgress(rec.InitialRecordedSpeed, rec.LastRecordedSpeed, rec.TargetMetronomeSpeed, rec.SpeedProgressWeighting);
                    var timeProgress = new PracticeTimeProgress(rec.TotalPracticeTime, rec.TargetPracticeTime, rec.PracticeTimeProgressWeighting);
                    var manualProgress = new ManualProgress(rec.LastRecordedManualProgress, rec.ManualProgressWeighting);

                    exerciseRecorders.Add(new ExerciseRecorder(new Recorder(), rec.ExerciseTitle, rec.TimeSlotTitle, speedProgress, timeProgress, manualProgress));
                }

                var practiceRoutineRecorder = new PracticeRoutineRecorder(practiceRoutine.Id, practiceRoutine.Title, exerciseRecorders);

                return practiceRoutineRecorder;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for id: {id}", ex);
            }
        }

        private void DeleteMissingRoutineExercises(PracticeRoutine entity)
        {
            var results = GetPracticeRoutineExercises(entity);

            var missingIds = results
                    .Select(a => a.ExerciseId)
                    .Except(entity.PracticeRoutineExercises.Select(a => a.ExerciseId));

            foreach (var id in missingIds)
            {
                Connection.Execute("sp_DeletePracticeRoutineExercise",
                    param: new
                    {
                        _practiceRoutineId = entity.Id,
                        _exerciseId = id
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        private IEnumerable<PracticeRoutineExercise> GetPracticeRoutineExercises(PracticeRoutine entity)
        {
            var routineExercises = Connection.Query<PracticeRoutineExercise>("sp_GetPracticeRoutineExercisesByPracticeRoutine",
            param: new
            {
                _practiceRoutineId = entity.Id
            }, commandType: CommandType.StoredProcedure);

            return routineExercises;
        }


        private void UpdateChangedPracticeRoutineExercises(PracticeRoutine entity)
        {
            var persistedRoutineExercises = GetPracticeRoutineExercises(entity);

            foreach (PracticeRoutineExercise routineExercise in entity.PracticeRoutineExercises)
            {
                if (routineExercise.PracticeRoutineId > 0 && routineExercise.ExerciseId > 0)
                {
                    var persistedCounterPart = persistedRoutineExercises.Where(p => p.ExerciseId == routineExercise.ExerciseId).SingleOrDefault();
                    if (persistedCounterPart != null)
                    {
                        CompareLogic compareLogic = new CompareLogic();
                        ComparisonResult result = compareLogic.Compare(persistedCounterPart, routineExercise);
                        if (!result.AreEqual)
                        {
                            UpdateRoutineExercise(routineExercise);
                        }
                    }
                }
            }
        }

        public void UpdateRoutineExercise(PracticeRoutineExercise routineExercise)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdatePracticeRoutineExercise",
                param: new
                {
                    _practiceRoutineId = routineExercise.PracticeRoutineId,
                    _exerciseId = routineExercise.ExerciseId,
                    _assignedPracticeTime = routineExercise.AssignedPracticeTime,
                    _difficultyRating = routineExercise.DifficultyRating,
                    _practicalityRating = routineExercise.PracticalityRating
                },
                commandType: CommandType.StoredProcedure
            );

            var updatedRoutineExercise = GetPracticeRoutineExerciseById(routineExercise.PracticeRoutineId, routineExercise.ExerciseId);
            routineExercise.DateModified = updatedRoutineExercise.DateModified;
        }

        private PracticeRoutineExercise GetPracticeRoutineExerciseById(int practiceRoutineId, int exerciseId)
        {
            try
            {
                var result = Connection.QuerySingle<PracticeRoutineExercise>("sp_GetPracticeRoutineExerciseById",
                    param: new
                    {
                        _practiceRoutineId = practiceRoutineId,
                        _exerciseId = exerciseId,
                    }, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for PracticeRoutineId: {practiceRoutineId} and ExerciseId: {exerciseId}", ex);
            }
        }

        private void InsertNewRoutineExercises(PracticeRoutine practiceRoutine)
        {
            foreach (PracticeRoutineExercise routineExercise in practiceRoutine.PracticeRoutineExercises)
            {
                if (routineExercise.PracticeRoutineId <= 0)
                {
                    Connection.ExecuteScalar<int>(sql: "sp_InsertPracticeRoutineExercise",
                    param: new
                    {
                        _practiceRoutineId = practiceRoutine.Id,
                        _exerciseId = routineExercise.ExerciseId,
                        _assignedPracticeTime = routineExercise.AssignedPracticeTime,
                        _difficultyRating = routineExercise.DifficultyRating,
                        _practicalityRating = routineExercise.PracticalityRating
                    },
                    commandType: CommandType.StoredProcedure
                    );
                }
            }
        }
    }
}