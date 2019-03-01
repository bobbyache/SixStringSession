using CygSoft.SmartSession.Dal.MySql.Common;
using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    //TODO: All objects updated should ensure that the entity that was sent in has the same values as the entity after it was updated in the database.

    [TestFixture]
    public class PracticeRoutineRepositoryTests
    {
        [Test]
        public void PracticeRoutineRepository_Insert_New_PracticeRoutine_Inserts_New_TimeSlotExercises()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exercise1 = EntityFactory.CreateExercise("Exercise 1");
                var exercise2 = EntityFactory.CreateExercise("Exercise 2");
                var exercise3 = EntityFactory.CreateExercise("Exercise 3");

                uow.Exercises.Add(exercise1);
                uow.Exercises.Add(exercise2);
                uow.Exercises.Add(exercise3);

                uow.Commit();

                TimeSlotExercise timeSlotExercise1 = new TimeSlotExercise(exercise1.Id, exercise1.Title, 3);
                TimeSlotExercise timeSlotExercise2 = new TimeSlotExercise(exercise2.Id, exercise2.Title, 3);
                TimeSlotExercise timeSlotExercise3 = new TimeSlotExercise(exercise3.Id, exercise3.Title, 3);

                List<TimeSlotExercise> timeSlotExercises1 = new List<TimeSlotExercise>
                {
                    timeSlotExercise1,
                    timeSlotExercise2
                };

                List<TimeSlotExercise> timeSlotExercises2 = new List<TimeSlotExercise>
                {
                    timeSlotExercise3
                };

                List<PracticeRoutineTimeSlot> timeSlots = new List<PracticeRoutineTimeSlot>
                {
                    new PracticeRoutineTimeSlot("Time Slot 1", 5, timeSlotExercises1),
                    new PracticeRoutineTimeSlot("Time Slot 2", 5, timeSlotExercises2),
                };

                PracticeRoutine practiceRoutine = new PracticeRoutine("Created PracticeRoutine", timeSlots);
                uow.PracticeRoutines.Add(practiceRoutine);
                uow.Commit();

                PracticeRoutine insertedPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                Assert.AreEqual(1, insertedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 1").Count());
                Assert.AreEqual(1, insertedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 2").Count());

                Assert.AreEqual(2, insertedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 1").First().Exercises.Count());
                Assert.AreEqual(1, insertedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 2").First().Exercises.Count());
            }
        }

        [Test]
        public void PracticeRoutineRepository_Insert_New_PracticeRoutine_Then_Update_Inserts_New_TimeSlotExercises()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine practiceRoutine = new PracticeRoutine("Created PracticeRoutine", new List<PracticeRoutineTimeSlot>());
                uow.PracticeRoutines.Add(practiceRoutine);
                uow.Commit();

                var exercise1 = EntityFactory.CreateExercise("Exercise 1");
                var exercise2 = EntityFactory.CreateExercise("Exercise 2");
                var exercise3 = EntityFactory.CreateExercise("Exercise 3");

                uow.Exercises.Add(exercise1);
                uow.Exercises.Add(exercise2);
                uow.Exercises.Add(exercise3);

                uow.Commit();

                TimeSlotExercise timeSlotExercise1 = new TimeSlotExercise(exercise1.Id, exercise1.Title, 3);
                TimeSlotExercise timeSlotExercise2 = new TimeSlotExercise(exercise2.Id, exercise2.Title, 3);
                TimeSlotExercise timeSlotExercise3 = new TimeSlotExercise(exercise3.Id, exercise3.Title, 3);

                List<TimeSlotExercise> timeSlotExercises1 = new List<TimeSlotExercise>
                {
                    timeSlotExercise1,
                    timeSlotExercise2
                };

                List<TimeSlotExercise> timeSlotExercises2 = new List<TimeSlotExercise>
                {
                    timeSlotExercise3
                };

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);
                existingPracticeRoutine.AddTimeSlot(new PracticeRoutineTimeSlot("Time Slot 1", 5, timeSlotExercises1));
                existingPracticeRoutine.AddTimeSlot(new PracticeRoutineTimeSlot("Time Slot 2", 5, timeSlotExercises2));
                uow.PracticeRoutines.Update(existingPracticeRoutine);
                uow.Commit();

                var updatedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                Assert.AreEqual(1, updatedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 1").Count());
                Assert.AreEqual(1, updatedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 2").Count());

                Assert.AreEqual(2, updatedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 1").First().Exercises.Count());
                Assert.AreEqual(1, updatedPracticeRoutine.TimeSlots.Where(t => t.Title == "Time Slot 2").First().Exercises.Count());
            }
        }

        [Test]
        public void PracticeRoutineRepository_Update_New_PracticeRoutine_Updates_New_TimeSlotExercises()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exercise1 = EntityFactory.CreateExercise("Exercise 1");
                var exercise2 = EntityFactory.CreateExercise("Exercise 2");

                uow.Exercises.Add(exercise1);
                uow.Exercises.Add(exercise2);

                uow.Commit();

                TimeSlotExercise timeSlotExercise1 = new TimeSlotExercise(exercise1.Id, exercise1.Title, 3);
                TimeSlotExercise timeSlotExercise2 = new TimeSlotExercise(exercise2.Id, exercise2.Title, 3);

                List<TimeSlotExercise> timeSlotExercises1 = new List<TimeSlotExercise>
                {
                    timeSlotExercise1,
                    timeSlotExercise2
                };

                List<PracticeRoutineTimeSlot> timeSlots = new List<PracticeRoutineTimeSlot>
                {
                    new PracticeRoutineTimeSlot("Time Slot 1", 300, timeSlotExercises1)
                };

                PracticeRoutine practiceRoutine = new PracticeRoutine("Created PracticeRoutine", timeSlots);
                uow.PracticeRoutines.Add(practiceRoutine);
                uow.Commit();

                PracticeRoutine insertedPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                var modifiedTimeSlot = insertedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Time Slot 1").Single();

                var modifiedExercise = insertedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Time Slot 1").Single()
                    .Exercises.Where(ex => ex.Title == "Exercise 1").ToList()
                    .First();

                modifiedTimeSlot.Title = "Modified Time Slot 1";
                modifiedTimeSlot.AssignedSeconds = 600;

                modifiedExercise.FrequencyWeighting = 10;

                uow.PracticeRoutines.Update(insertedPracticeRoutine);
                uow.Commit();

                var modifiedPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                var changedTimeSlot = modifiedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Modified Time Slot 1").Single();

                var exerciseCount = modifiedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Exercises.Count();

                var changedExercise = modifiedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Exercises.Where(ex => ex.Title == "Exercise 1")
                    .SingleOrDefault();


                var unchangedExercise = modifiedPracticeRoutine
                    .TimeSlots.Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Exercises.Where(ex => ex.Title == "Exercise 2")
                    .SingleOrDefault();

                Assert.That(changedTimeSlot.AssignedSeconds, Is.EqualTo(600));

                Assert.That(changedExercise.FrequencyWeighting, Is.EqualTo(10));
                Assert.That(unchangedExercise.FrequencyWeighting, Is.EqualTo(3));

                Assert.That(exerciseCount, Is.EqualTo(2));

            }
        }

        [Test]
        public void PracticeRoutineRepository_Find_PracticeRoutine_With_Specific_Title_Gets_Applicable_Recs()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data-practiceroutines.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IPracticeRoutineSearchCriteria crit = new PracticeRoutineSearchCriteria();
                crit.Title = "monday";

                var practiceRoutines = uow.PracticeRoutines.Find(crit);
                Assert.That(practiceRoutines.Where(ex => ex.Title == "Monday Routine").SingleOrDefault(), Is.Not.Null);
                Assert.IsInstanceOf<PracticeRoutineHeader>(practiceRoutines.First());
            }
        }

        [Test]
        public void PracticeRoutineRepository_Get_PracticeRoutine_Fetches_TimeSlots()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data-practiceroutine-recorder.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IPracticeRoutineSearchCriteria crit = new PracticeRoutineSearchCriteria();
                crit.Title = "monday";

                var practiceRoutineHeader = uow.PracticeRoutines.Find(crit).First();
                var practiceRoutine = uow.PracticeRoutines.Get(practiceRoutineHeader.Id);

                Assert.IsTrue(practiceRoutine.TimeSlots.Count > 0);
                Assert.AreEqual(6, practiceRoutine.TimeSlots.Count);
                Assert.AreEqual(4, practiceRoutine.TimeSlots[0].Exercises.Count());
            }
        }

        [Test]
        public void PracticeRoutineRecorder_Fetch_And_Create_Is_Constructed_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data-practiceroutine-recorder.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IPracticeRoutineSearchCriteria crit = new PracticeRoutineSearchCriteria();
                crit.Title = "monday";

                var practiceRoutine = uow.PracticeRoutines.Find(crit).SingleOrDefault();
                PracticeRoutineRecorder practiceRoutineRecorder = uow.PracticeRoutines.GetPracticeRoutineRecorder(practiceRoutine.Id);

                Assert.IsNotNull(practiceRoutineRecorder);
                Assert.That(practiceRoutineRecorder.Title, Is.EqualTo("Monday Routine"));
                Assert.That(practiceRoutineRecorder.ItemCount, Is.EqualTo(6));
                Assert.That(practiceRoutineRecorder.ExerciseRecorders.Length, Is.EqualTo(6));

                Assert.AreEqual(60, practiceRoutineRecorder.ExerciseRecorders[0].CurrentTotalSeconds);
            }
        }

        

        [Test]
        public void PracticeRoutineRepository_Creates_A_New_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine existingPracticeRoutine;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                uow.PracticeRoutines.Remove(newPracticeRoutine);
                uow.Commit();
            }

            Assert.IsNotNull(existingPracticeRoutine);

            Assert.That(existingPracticeRoutine.Title, Is.EqualTo("New Practice Routine"));
            Assert.That(existingPracticeRoutine.DateCreated, Is.Not.Null);
            Assert.That(existingPracticeRoutine.DateModified, Is.Null);
        }

        [Test]
        public void PracticeRoutineRepository_Updates_An_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine modifiedPracticeRoutine;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                existingPracticeRoutine.Title = "Modified PracticeRoutine Title";

                uow.PracticeRoutines.Update(existingPracticeRoutine);
                uow.Commit();

                modifiedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                uow.PracticeRoutines.Remove(existingPracticeRoutine);
                uow.Commit();
            }

            Assert.IsNotNull(modifiedPracticeRoutine);

            Assert.That(modifiedPracticeRoutine.Title, Is.EqualTo("Modified PracticeRoutine Title"));
            Assert.That(modifiedPracticeRoutine.DateCreated, Is.Not.Null);
            Assert.That(modifiedPracticeRoutine.DateModified, Is.Not.Null);
            Assert.That(modifiedPracticeRoutine.DateModified, Is.GreaterThanOrEqualTo(currentTime));
        }

        [Test]
        public void PracticeRoutineRepository_UnitOfWork_AddAndModify_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            PracticeRoutine modifiedPracticeRoutine;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(newPracticeRoutine);
                // --- do not commit !!!

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);

                existingPracticeRoutine.Title = "Modified PracticeRoutine Title";

                uow.PracticeRoutines.Update(existingPracticeRoutine);
                // --- do not commit !!!
                modifiedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                uow.Rollback();

                ActualValueDelegate<PracticeRoutine> testDelegate = () => uow.PracticeRoutines.Get(modifiedPracticeRoutine.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        [Test]
        public void PracticeRoutineRepository_UnitOfWork_Delete_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                PracticeRoutine newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(newPracticeRoutine);
                uow.Commit();

                uow.PracticeRoutines.Remove(newPracticeRoutine);
                uow.Rollback(); // don't delete.

                // fail here if the PracticeRoutine has been deleted.
                var notDeletedPracticeRoutine = uow.PracticeRoutines.Get(newPracticeRoutine.Id);
            }
        }

        [Test]
        public void PracticeRoutineRepository_Deletes_A_New_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newEx = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(newEx);
                uow.Commit();

                uow.PracticeRoutines.Remove(newEx);
                uow.Commit();

                ActualValueDelegate<PracticeRoutine> testDelegate = () => uow.PracticeRoutines.Get(newEx.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        [Test]
        public void PracticeRoutineRepository_Creates_A_New_PracticeRoutineExercise_With_A_New_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var createdExercise = CreateExercise();
                uow.Exercises.Add(createdExercise);
                uow.Commit();

                var createdPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");

                createdPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
                {
                    ExerciseId = createdExercise.Id,
                    AssignedPracticeTime = 5000,
                    FrequencyWeighting = 3
                });

                uow.PracticeRoutines.Add(createdPracticeRoutine);
                uow.Commit();

                var practiceRoutine = uow.PracticeRoutines.Get(createdPracticeRoutine.Id);
                var practiceRoutineExercise = practiceRoutine.PracticeRoutineExercises[0];

                Assert.That(practiceRoutineExercise.ExerciseId, Is.GreaterThan(0));
                Assert.That(practiceRoutineExercise.FrequencyWeighting, Is.EqualTo(3));
                Assert.That(practiceRoutineExercise.DateModified, Is.Null);
                Assert.That(practiceRoutineExercise.DateCreated, Is.Not.Null);
                Assert.That(practiceRoutineExercise.AssignedPracticeTime, Is.EqualTo(5000));

                Assert.That(createdPracticeRoutine.Id, Is.GreaterThan(0));
                Assert.That(createdPracticeRoutine.DateCreated, Is.Not.Null);
                Assert.That(createdPracticeRoutine.DateModified, Is.Null);

                Assert.That(createdPracticeRoutine.Id, Is.EqualTo(practiceRoutineExercise.PracticeRoutineId));
            }
        }

        [Test]
        public void PracticeRoutineRepository_Inserts_A_New_PracticeRoutineExercise_With_An_Existing_PracticeRoutine_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var createdExercise = CreateExercise();
                uow.Exercises.Add(createdExercise);
                uow.Commit();

                var createdPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(createdPracticeRoutine);
                uow.Commit();

                var existingPracticeRoutine = uow.PracticeRoutines.Get(createdPracticeRoutine.Id);

                existingPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
                {
                    ExerciseId = createdExercise.Id,
                    AssignedPracticeTime = 5000,
                    FrequencyWeighting = 3
                });

                uow.PracticeRoutines.Update(existingPracticeRoutine);

                var updatedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);
                var practiceRoutineExercise = updatedPracticeRoutine.PracticeRoutineExercises[0];

                Assert.That(practiceRoutineExercise.ExerciseId, Is.GreaterThan(0));
                Assert.That(practiceRoutineExercise.FrequencyWeighting, Is.EqualTo(3));
                Assert.That(practiceRoutineExercise.DateModified, Is.Null);
                Assert.That(practiceRoutineExercise.DateCreated, Is.Not.Null);
                Assert.That(practiceRoutineExercise.AssignedPracticeTime, Is.EqualTo(5000));

                Assert.That(updatedPracticeRoutine.Id, Is.GreaterThan(0));
                Assert.That(updatedPracticeRoutine.DateCreated, Is.Not.Null);
                Assert.That(updatedPracticeRoutine.DateModified, Is.Not.Null);

                Assert.That(updatedPracticeRoutine.Id, Is.EqualTo(practiceRoutineExercise.PracticeRoutineId));
            }
        }

        [Test]
        public void PracticeRoutineRepository_Deletes_Missing_PracticeRoutineExercise()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var createdExercise = CreateExercise();
                uow.Exercises.Add(createdExercise);
                uow.Commit();

                var createdPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                uow.PracticeRoutines.Add(createdPracticeRoutine);
                uow.Commit();

                var existingPracticeRoutine = uow.PracticeRoutines.Get(createdPracticeRoutine.Id);

                existingPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
                {
                    ExerciseId = createdExercise.Id,
                    AssignedPracticeTime = 5000,
                    FrequencyWeighting = 3
                });

                uow.PracticeRoutines.Update(existingPracticeRoutine);

                var updatedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);
                updatedPracticeRoutine.PracticeRoutineExercises.Remove(updatedPracticeRoutine.PracticeRoutineExercises[0]);

                var routineExerciseCountAfterDeletion = updatedPracticeRoutine.PracticeRoutineExercises.Count;

                uow.PracticeRoutines.Update(updatedPracticeRoutine);
                var finallyUpdatedPracticeRoutine = uow.PracticeRoutines.Get(updatedPracticeRoutine.Id);

                Assert.That(routineExerciseCountAfterDeletion, Is.EqualTo(0));
                Assert.That(finallyUpdatedPracticeRoutine.PracticeRoutineExercises.Count, Is.EqualTo(0));
            }
        }

        [Test]
        public void PracticeRoutineRepository_Updates_An_Existing_PracticeRoutineExercise_Only_If_Changed()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var createdExercise1 = CreateExercise();
                var createdExercise2 = CreateExercise();
                uow.Exercises.Add(createdExercise1);
                uow.Exercises.Add(createdExercise2);
                uow.Commit();

                var createdPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
                createdPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
                {
                    ExerciseId = createdExercise1.Id,
                    AssignedPracticeTime = 5000,
                    FrequencyWeighting = 2
                });

                createdPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
                {
                    ExerciseId = createdExercise2.Id,
                    AssignedPracticeTime = 10000,
                    FrequencyWeighting = 5
                });

                uow.PracticeRoutines.Add(createdPracticeRoutine);
                uow.Commit();

                var practiceRoutine = uow.PracticeRoutines.Get(createdPracticeRoutine.Id);
                var practiceRoutineExercise1 = practiceRoutine.PracticeRoutineExercises[0];
                var practiceRoutineExercise2 = practiceRoutine.PracticeRoutineExercises[1];

                practiceRoutineExercise1.FrequencyWeighting = 3;
                uow.PracticeRoutines.Update(practiceRoutine);

                var practiceRoutineUnderTest = uow.PracticeRoutines.Get(practiceRoutine.Id);
                var practiceRoutineExerciseUnderTest1 = practiceRoutine.PracticeRoutineExercises.Where(pr => pr.ExerciseId == practiceRoutineExercise1.ExerciseId).Single();
                var practiceRoutineExerciseUnderTest2 = practiceRoutine.PracticeRoutineExercises.Where(pr => pr.ExerciseId == practiceRoutineExercise2.ExerciseId).Single();

                Assert.That(practiceRoutineExerciseUnderTest1.DateModified, Is.Not.Null);
                Assert.That(practiceRoutineExerciseUnderTest2.DateModified, Is.Null);
            }
        }

        private Exercise CreateExercise()
        {
            Exercise exercise = new Exercise
            {
                TargetMetronomeSpeed = 150,
                TargetPracticeTime = 50000,
                Title = "Created Exercise Title"
            };

            return exercise;
        }

        private Exercise GetExercise()
        {
            Exercise exercise = new Exercise
            {
                Id = 3,
                DateCreated = DateTime.Parse("2018-03-01 12:15:00"),
                DateModified = DateTime.Parse("2018-03-01 12:15:00"),
                TargetMetronomeSpeed = 150,
                TargetPracticeTime = 50000,
                Title = "Created Exercise Title"
            };

            return exercise;
        }
    }
}
