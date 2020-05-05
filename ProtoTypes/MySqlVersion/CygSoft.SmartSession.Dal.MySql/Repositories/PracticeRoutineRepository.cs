using CygSoft.SmartSession.Dal.MySql.Common;
using CygSoft.SmartSession.Dal.MySql.PracticeRoutines;
using CygSoft.SmartSession.Dal.MySql.PracticeRoutines.Records;
using CygSoft.SmartSession.Dal.MySql.Records;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using Dapper;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CygSoft.SmartSession.Dal.MySql.Repositories
{
    public class PracticeRoutineRepository : RepositoryBase, IPracticeRoutineRepository
    {
        public PracticeRoutineRepository(IDbConnection connection) : base(connection)
        {
        }

        public void Add(PracticeRoutine entity)
        {
            entity.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertPracticeRoutine",
                param: new
                {
                    _title = entity.Title
                },
                commandType: CommandType.StoredProcedure
                );

            InsertNewTimeSlots(entity);
            InsertNewTimeSlotExercises(entity);

            var persistedEntity = Get(entity.Id);
            entity.DateCreated = persistedEntity.DateCreated;
        }

        public void AddRange(IEnumerable<PracticeRoutine> entities)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<PracticeRoutineHeader> Find(IPracticeRoutineSearchCriteria criteria)
        {
            var crit = criteria;

            var records = Connection.Query<PracticeRoutineRecord>("sp_FindPracticeRoutines",
                param: new
                {
                    _title = crit.Title,
                    _fromDateCreated = crit.FromDateCreated,
                    _toDateCreated = crit.ToDateCreated,
                    _fromDateModified = crit.FromDateModified,
                    _toDateModified = crit.ToDateModified
                }, commandType: CommandType.StoredProcedure);

            List<PracticeRoutineHeader> headers = new List<PracticeRoutineHeader>();
            foreach (var record in records)
                headers.Add(new PracticeRoutineHeader(record.Id, record.Title, record.DateCreated, record.DateModified));

            return headers;
        }


        public IEnumerable<TimeSlotExerciseRecord> GetTimeSlotExercisesByTimeSlotIds(string commaDelimitedIds)
        {
            var timeSlotExerciseRecords = Connection.Query<TimeSlotExerciseRecord>("sp_GetTimeSlotExerciseByTimeSlotIds",
            param: new
            {
                _ids = commaDelimitedIds
            }, commandType: CommandType.StoredProcedure);

            return timeSlotExerciseRecords;
        }

        private string GetCommaDelimitedIds(IEnumerable<IIdentityItem> items)
        {
            return string.Join(",", items.Select(item => item.Id).ToArray());
        }

        private string GetCommaDelimitedIds(IEnumerable<int> itemIds)
        {
            return string.Join(",", itemIds.ToArray());
        }

        public PracticeRoutine Get(int id)
        {
            try
            {
                var practiceRoutineRecord = Connection.QuerySingle<PracticeRoutineRecord>("sp_GetPracticeRoutineById",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);


                var timeSlotRecords = Connection.Query<TimeSlotRecord>("sp_GetTimeSlotsByPracticeRoutineId",
                 param: new
                 {
                     _id = id
                 }, commandType: CommandType.StoredProcedure);

                var timeSlotExerciseRecords = GetTimeSlotExercisesByTimeSlotIds(GetCommaDelimitedIds(timeSlotRecords));

                var timeSlots = new List<PracticeRoutineTimeSlot>();

                foreach (var timeSlotRecord in timeSlotRecords)
                {
                    var timeSlotExerciseRecs = timeSlotExerciseRecords
                        .Where(recs => recs.TimeSlotId == timeSlotRecord.Id)
                        .Select(rec => new TimeSlotExercise(rec.Id, rec.TimeSlotId, rec.Title, rec.FrequencyWeighting) { TimeSlotId = rec.TimeSlotId } ).ToList();

                    var timeSlot = new PracticeRoutineTimeSlot(timeSlotRecord.Id, timeSlotRecord.Title, timeSlotRecord.AssignedPracticeTime,
                        timeSlotExerciseRecs);

                    timeSlots.Add(timeSlot);
                }

                var practiceRoutine = new PracticeRoutine(practiceRoutineRecord.Id, practiceRoutineRecord.Title, timeSlots);

                practiceRoutine.DateCreated = practiceRoutineRecord.DateCreated;
                practiceRoutine.DateModified = practiceRoutineRecord.DateModified;

                return practiceRoutine;
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

            InsertNewTimeSlots(entity);
            InsertNewTimeSlotExercises(entity);
            UpdateExistingTimeSlots(entity);
            UpdateExistingTimeSlotExercises(entity);

            DeleteMissingTimeSlotExercises(entity);
            DeleteMissingTimeSlots(entity);

            var persistedEntity = Get(entity.Id);
            entity.DateCreated = persistedEntity.DateCreated;
        }


        private void UpdateExistingTimeSlots(PracticeRoutine entity)
        {
            var dbPracticeRoutine = Get(entity.Id);

            foreach (var timeSlot in entity)
            {
                if (timeSlot.Id > 0)
                {
                    var persistedCounterPart = dbPracticeRoutine.Where(p => p.Id == timeSlot.Id).SingleOrDefault();
                    if (persistedCounterPart != null)
                    {
                        CompareLogic compareLogic = new CompareLogic();
                        ComparisonResult result = compareLogic.Compare(persistedCounterPart, timeSlot);
                        if (!result.AreEqual)
                        {
                            UpdateTimeSlot(timeSlot);
                        }
                    }
                }
            }
        }

        private void DeleteMissingTimeSlots(PracticeRoutine entity)
        {
            var results = Get(entity.Id);

            var missingIds = results
                    .Select(a => a.Id)
                    .Except(entity.Select(a => a.Id));

            foreach (var id in missingIds)
            {
                Connection.Execute("sp_DeleteTimeSlot",
                    param: new { _id = id }, commandType: CommandType.StoredProcedure);
            }
        }


        private void DeleteMissingTimeSlotExercises(PracticeRoutine entity)
        {
            foreach (var timeSlot in entity)
            {
                var results = Connection.Query<TimeSlotExerciseRecord>("sp_GetTimeSlotExerciseByTimeSlotId",
                 param: new
                 {
                     _id = timeSlot.Id
                 }, commandType: CommandType.StoredProcedure);

                var missingIds = results
                        .Select(a => a.Id)
                        .Except(timeSlot.Select(a => a.Id));

                Connection.Execute("sp_DeleteTimeSlotExercisesByIds",
                    param: new { _timeSlotId = timeSlot.Id, _exerciseIds = GetCommaDelimitedIds(missingIds) }, commandType: CommandType.StoredProcedure);
            }
        }


        public void UpdateTimeSlot(PracticeRoutineTimeSlot timeSlot)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdateTimeSlot",
                param: new
                {
                    _id = timeSlot.Id,
                    _title = timeSlot.Title,
                    _assignedPracticeTime = timeSlot.AssignedSeconds
                },
                commandType: CommandType.StoredProcedure
            );
        }

        private void UpdateExistingTimeSlotExercises(PracticeRoutine entity)
        {
            var timeSlotExerciseRecords = GetTimeSlotExercisesByTimeSlotIds(GetCommaDelimitedIds(entity));

            foreach (var timeSlot in entity)
            {
                foreach (var exercise in timeSlot)
                {
                    if (exercise.Id > 0 && exercise.TimeSlotId > 0)
                    {
                        var persistedCounterPart = timeSlotExerciseRecords.Where(p => p.Id == exercise.Id && p.TimeSlotId == timeSlot.Id).SingleOrDefault();
                        if (persistedCounterPart != null)
                        {
                            var equal = (
                                persistedCounterPart.Title == exercise.Title &&
                                persistedCounterPart.FrequencyWeighting == exercise.FrequencyWeighting
                                );

                            // TODO: Find a better way to compare whether the values of an object are equal (when the object is of different types).
                            if (!equal) UpdateTimeSlotExercise(exercise);
                        }
                    }
                }
            }
        }

        public void UpdateTimeSlotExercise(TimeSlotExercise exercise)
        {
            Connection.ExecuteScalar<int>(sql: "sp_UpdateTimeSlotExercise",
                param: new
                {
                    _exerciseId = exercise.Id,
                    _timeSlotId = exercise.TimeSlotId,
                    _frequencyWeighting = exercise.FrequencyWeighting
                },
                commandType: CommandType.StoredProcedure
            );
        }

        private void InsertNewTimeSlotExercises(PracticeRoutine practiceRoutine)
        {
            foreach (var timeSlot in practiceRoutine)
            {
                foreach (var exercise in timeSlot)
                {
                    if (exercise.TimeSlotId == 0 && exercise.Id > 0)
                    {
                        exercise.TimeSlotId = timeSlot.Id;

                        Connection.ExecuteScalar<int>(sql: "sp_InsertTimeSlotExercise",
                        param: new
                        {
                            _exerciseId = exercise.Id,
                            _timeSlotId = timeSlot.Id,
                            _frequencyWeighting = exercise.FrequencyWeighting
                        },
                        commandType: CommandType.StoredProcedure
                        );
                    }
                }
            }
        }

        private void InsertNewTimeSlots(PracticeRoutine practiceRoutine)
        {
            foreach (var timeSlot in practiceRoutine)
            {
                if (timeSlot.Id <= 0)
                {
                    timeSlot.Id = Connection.ExecuteScalar<int>(sql: "sp_InsertTimeSlot",
                    param: new
                    {
                        _title = timeSlot.Title,
                        _practiceRoutineId = practiceRoutine.Id,
                        _timeSlotId = timeSlot.Id,
                        _assignedPracticeTime = timeSlot.AssignedSeconds
                    },
                    commandType: CommandType.StoredProcedure
                    );
                }
            }
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

                List<TimeSlotExerciseRecorder> exerciseRecorders = new List<TimeSlotExerciseRecorder>();

                foreach (var rec in exerciseRecorderRecords)
                {
                    var speedProgress = new SpeedProgress(rec.InitialRecordedSpeed, rec.LastRecordedSpeed, rec.TargetMetronomeSpeed, rec.SpeedProgressWeighting);
                    var timeProgress = new PracticeTimeProgress(rec.TotalPracticeTime, rec.TargetPracticeTime, rec.PracticeTimeProgressWeighting);
                    var manualProgress = new ManualProgress(rec.LastRecordedManualProgress, rec.ManualProgressWeighting);
                    
                    exerciseRecorders.Add(new TimeSlotExerciseRecorder(new Recorder(), rec.ExerciseId, $"{rec.TimeSlotTitle} : {rec.ExerciseTitle}", speedProgress, timeProgress, manualProgress, rec.AssignedPracticeTime));
                }

                var practiceRoutineRecorder = new PracticeRoutineRecorder(practiceRoutine.Id, practiceRoutine.Title, exerciseRecorders);

                return practiceRoutineRecorder;
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseEntityNotFoundException($"Database entity does not exist for id: {id}", ex);
            }
        }

        // TODO: Remove Find from the IRepository interface. It should return header objects and not the repository objects. Then remove all of these methods and replace them with specialized ones.
        public IReadOnlyList<PracticeRoutine> Find(object criteria)
        {
            throw new NotImplementedException();
        }
    }
}