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

                Assert.AreEqual(1, insertedPracticeRoutine.Where(t => t.Title == "Time Slot 1").Count());
                Assert.AreEqual(1, insertedPracticeRoutine.Where(t => t.Title == "Time Slot 2").Count());

                Assert.AreEqual(2, insertedPracticeRoutine.Where(t => t.Title == "Time Slot 1").First().Count());
                Assert.AreEqual(1, insertedPracticeRoutine.Where(t => t.Title == "Time Slot 2").First().Count());
            }
        }

        [Test]
        public void PracticeRoutineRepository_Existing_PracticeRoutine_Deletes_Children_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exercise1 = EntityFactory.CreateExercise("Exercise 1");
                var exercise2 = EntityFactory.CreateExercise("Exercise 2");
                var exercise3 = EntityFactory.CreateExercise("Exercise 3");
                var exercise4 = EntityFactory.CreateExercise("Exercise 4");

                uow.Exercises.Add(exercise1);
                uow.Exercises.Add(exercise2);
                uow.Exercises.Add(exercise3);
                uow.Exercises.Add(exercise4);

                uow.Commit();

                TimeSlotExercise timeSlotExercise1 = new TimeSlotExercise(exercise1.Id, exercise1.Title, 3);
                TimeSlotExercise timeSlotExercise2 = new TimeSlotExercise(exercise2.Id, exercise2.Title, 3);
                TimeSlotExercise timeSlotExercise3 = new TimeSlotExercise(exercise3.Id, exercise3.Title, 3);
                TimeSlotExercise timeSlotExercise4 = new TimeSlotExercise(exercise4.Id, exercise4.Title, 3);

                List<TimeSlotExercise> timeSlotExercises1 = new List<TimeSlotExercise>
                {
                    timeSlotExercise1,
                    timeSlotExercise2
                };

                List<TimeSlotExercise> timeSlotExercises2 = new List<TimeSlotExercise>
                {
                    timeSlotExercise3
                };

                List<TimeSlotExercise> timeSlotExercises3 = new List<TimeSlotExercise>
                {
                    timeSlotExercise4
                };

                List<PracticeRoutineTimeSlot> timeSlots = new List<PracticeRoutineTimeSlot>
                {
                    new PracticeRoutineTimeSlot("Time Slot 1", 300, timeSlotExercises1),
                    new PracticeRoutineTimeSlot("Time Slot 2", 300, timeSlotExercises2),
                    new PracticeRoutineTimeSlot("Time Slot 3", 300, timeSlotExercises3)
                };

                PracticeRoutine practiceRoutine = new PracticeRoutine("Created PracticeRoutine", timeSlots);
                uow.PracticeRoutines.Add(practiceRoutine);
                uow.Commit();

                PracticeRoutine existingPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                // --------------------- delete some ----------------------------------------------------

                existingPracticeRoutine.Remove(existingPracticeRoutine.Where(ts => ts.Title == "Time Slot 3").Single());

                var removeExercise = existingPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 1").Single()
                    .Where(ex => ex.Title == "Exercise 1").Single();

                existingPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 1").Single()
                    .Remove(removeExercise);

                // --------------------- delete some ----------------------------------------------------

                uow.PracticeRoutines.Update(existingPracticeRoutine);
                uow.Commit();

                var modifiedPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                var timeSlotCount = modifiedPracticeRoutine.Count();  // should be one less after deletion of timeslot.

                var removedExercise = modifiedPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 1").Single()
                    .Where(ex => ex.Title == "Exercise 1").SingleOrDefault();

                var timeSlot1ExerciseCount = modifiedPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 1").Single()
                    .Count; // should be one less after deletion of exercise.

                var timeSlot1RemainingExercise = modifiedPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 1").Single()
                    .Single(); // should be one less after deletion of exercise.

                var timeSlot2Exercise = modifiedPracticeRoutine
                    .Where(ts => ts.Title == "Time Slot 2").Single()
                    .Single(); // should be one less after deletion of exercise.

                Assert.AreEqual(2, timeSlotCount, "There should only be two time slots left (Time Slot 3 and its children have been deleted");

                Assert.AreEqual(1, timeSlot1ExerciseCount);
                Assert.That(timeSlot1RemainingExercise.Title, Is.EqualTo("Exercise 2"), "The last remaining exercise in Time Slot 1 should be Exercise 2");
                Assert.That(timeSlot2Exercise.Title, Is.EqualTo("Exercise 3"), "The exercise in Time Slot 2 should be Exercise 3");
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
                existingPracticeRoutine.Add(new PracticeRoutineTimeSlot("Time Slot 1", 5, timeSlotExercises1));
                existingPracticeRoutine.Add(new PracticeRoutineTimeSlot("Time Slot 2", 5, timeSlotExercises2));
                uow.PracticeRoutines.Update(existingPracticeRoutine);
                uow.Commit();

                var updatedPracticeRoutine = uow.PracticeRoutines.Get(existingPracticeRoutine.Id);

                Assert.AreEqual(1, updatedPracticeRoutine.Where(t => t.Title == "Time Slot 1").Count());
                Assert.AreEqual(1, updatedPracticeRoutine.Where(t => t.Title == "Time Slot 2").Count());

                Assert.AreEqual(2, updatedPracticeRoutine.Where(t => t.Title == "Time Slot 1").First().Count());
                Assert.AreEqual(1, updatedPracticeRoutine.Where(t => t.Title == "Time Slot 2").First().Count());
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
                    .Where(tslot => tslot.Title == "Time Slot 1").Single();

                var modifiedExercise = insertedPracticeRoutine
                    .Where(tslot => tslot.Title == "Time Slot 1").Single()
                    .Where(ex => ex.Title == "Exercise 1").ToList()
                    .First();

                modifiedTimeSlot.Title = "Modified Time Slot 1";
                modifiedTimeSlot.AssignedSeconds = 600;

                modifiedExercise.FrequencyWeighting = 10;

                uow.PracticeRoutines.Update(insertedPracticeRoutine);
                uow.Commit();

                var modifiedPracticeRoutine = uow.PracticeRoutines.Get(practiceRoutine.Id);

                var changedTimeSlot = modifiedPracticeRoutine
                    .Where(tslot => tslot.Title == "Modified Time Slot 1").Single();

                var exerciseCount = modifiedPracticeRoutine
                    .Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Count();

                var changedExercise = modifiedPracticeRoutine
                    .Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Where(ex => ex.Title == "Exercise 1")
                    .SingleOrDefault();


                var unchangedExercise = modifiedPracticeRoutine
                    .Where(tslot => tslot.Title == "Modified Time Slot 1").Single()
                    .Where(ex => ex.Title == "Exercise 2")
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

                Assert.IsTrue(practiceRoutine.Count > 0);
                Assert.AreEqual(6, practiceRoutine.Count);
                Assert.AreEqual(4, practiceRoutine[0].Count());
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
