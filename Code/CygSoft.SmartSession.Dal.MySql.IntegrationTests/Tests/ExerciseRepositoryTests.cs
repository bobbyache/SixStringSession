using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure.Enums;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Linq;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class ExerciseRepositoryTests
    {
        [Test]
        public void ExerciseRepository_Find_Exercise_With_Specific_Title_Gets_Applicable_Recs()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.Title = "reen exercis";

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise").SingleOrDefault(), Is.Not.Null);
                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 1").SingleOrDefault(), Is.Not.Null);
                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 2").SingleOrDefault(), Is.Not.Null);
            }
        }

        //TODO:This needs to be implemented.
        //[Test]
        //public void ExerciseRepository_Find_Exercise_With_Specific_CalcPercentType_Gets_Applicable_Recs()
        //{
        //    Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
        //    Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

        //    //using (var uow = new UnitOfWork(Settings.AppConnectionString))
        //    //{
        //    //    IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
        //    //    crit.PercentageCompleteCalculationType = Metronome

        //    //    var exercises = uow.Exercises.Find(crit);
        //    //    Assert.That(exercises.Where(ex => ex.Title == "Orange Exercise").SingleOrDefault(), Is.Not.Null);
        //    //    Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 1").SingleOrDefault(), Is.Not.Null);
        //    //    Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 2").SingleOrDefault(), Is.Not.Null);
        //    //}

        //    throw new NotImplementedException();
        //}

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateModified_Before_ToDateModified()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);
            
            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.ToDateModified = new DateTime(2017, 5, 1);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.SingleOrDefault, Is.Not.Null);
                Assert.That(exercises.SingleOrDefault().Title == "Green Exercise 1");
            }
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateCreated_Before_ToDateCreated()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.ToDateCreated = new DateTime(2015, 5, 2);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.Count(), Is.EqualTo(2));

                Assert.That(exercises.Where(ex => ex.Title == "Yellow Exercise").SingleOrDefault(), Is.Not.Null);
                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise").SingleOrDefault(), Is.Not.Null);
            }
        }

        [Test]
        public void ExerciseRepository_Get_Exercises_By_PracticeRoutine()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exercise1 = EntityFactory.CreateExercise(title: "Yellow Exercise", targetSpeed: 120, targetpracticeTime: 300);
                var exercise2 = EntityFactory.CreateExercise(title: "Green Exercise", targetSpeed: 120, targetpracticeTime: 300);
                var exercise3 = EntityFactory.CreateExercise(title: "Blue Exercise", targetSpeed: 120, targetpracticeTime: 300);

                exercise1.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());
                exercise1.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());
                exercise2.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());
                exercise2.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());

                uow.Exercises.Add(exercise1);
                uow.Exercises.Add(exercise2);
                uow.Exercises.Add(exercise3);
                uow.Commit();

                var practiceRoutine1 = EntityFactory.CreatePracticeRoutine("Yellow Routine");
                var practiceRoutine2 = EntityFactory.CreatePracticeRoutine("Not Important Routine");

                practiceRoutine1.PracticeRoutineExercises.Add(EntityFactory.CreatePracticeRoutineExercise(exercise1));
                practiceRoutine1.PracticeRoutineExercises.Add(EntityFactory.CreatePracticeRoutineExercise(exercise2));
                practiceRoutine2.PracticeRoutineExercises.Add(EntityFactory.CreatePracticeRoutineExercise(exercise3));

                uow.PracticeRoutines.Add(practiceRoutine1);
                uow.PracticeRoutines.Add(practiceRoutine2);

                uow.Commit();

                var exercises = uow.Exercises.GetPracticeRoutineExercises(practiceRoutine1.Id);

                Assert.That(exercises.Count, Is.EqualTo(2));
                Assert.That(exercises[0].ExerciseActivity.Count, Is.EqualTo(2));
                Assert.That(exercises[1].ExerciseActivity.Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateModified_On_Or_After_ToDateModified()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.FromDateModified = new DateTime(2017, 6, 1);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.SingleOrDefault, Is.Not.Null);
                Assert.That(exercises.SingleOrDefault().Title == "Green Exercise 2");
            }
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateCreated_On_Or_After_ToDateCreated()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.FromDateCreated = new DateTime(2017, 1, 29);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.Count(), Is.EqualTo(2));

                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 1").SingleOrDefault(), Is.Not.Null);
                Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 2").SingleOrDefault(), Is.Not.Null);
            }
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateModified_After_FromDateModified_But_After_ToDateModified()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.FromDateModified = new DateTime(2017, 3, 31);
                crit.ToDateModified = new DateTime(2017, 4, 2);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.SingleOrDefault, Is.Not.Null);
                Assert.That(exercises.SingleOrDefault().Title == "Green Exercise 1");
            }
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateCreated_After_FromDateCreated_But_After_ToDateCreated()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("test-data.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
                crit.FromDateCreated = new DateTime(2015, 11, 25);
                crit.ToDateCreated = new DateTime(2016, 2, 2);

                var exercises = uow.Exercises.Find(crit);
                Assert.That(exercises.Where(ex => ex.Title == "Blue Exercise").SingleOrDefault(), Is.Not.Null);
                Assert.That(exercises.Where(ex => ex.Title == "Orange Exercise").SingleOrDefault(), Is.Not.Null);
            }
        }


        [Test]
        public void ExerciseRepository_Creates_A_New_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise nexExercise;
            Exercise persistedExercise;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                nexExercise = CreateMetronomeExercise();
                uow.Exercises.Add(nexExercise);
                uow.Commit();

                persistedExercise = (Exercise)uow.Exercises.Get(nexExercise.Id);

                uow.Exercises.Remove(nexExercise);
                uow.Commit();
            }

            Assert.IsNotNull(persistedExercise);

            Assert.That(nexExercise.DateCreated, Is.Not.Null);
            Assert.That(nexExercise.DateModified, Is.Null);
            Assert.That(nexExercise.Id, Is.GreaterThan(0));

            Assert.That(persistedExercise.Title, Is.EqualTo("Created Exercise Title"));
            Assert.That(persistedExercise.TargetMetronomeSpeed, Is.EqualTo(150));
            Assert.That(persistedExercise.SpeedProgressWeighting, Is.EqualTo(50));
            Assert.That(persistedExercise.PracticeTimeProgressWeighting, Is.EqualTo(50));
            Assert.That(persistedExercise.TargetPracticeTime, Is.Null);
            Assert.That(persistedExercise.DateCreated, Is.Not.Null);
            Assert.That(persistedExercise.DateModified, Is.Null);
        }

        [Test]
        public void ExerciseRepository_Updates_An_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise modifiedExercise;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);
                uow.Commit();

                Exercise existingExercise = (Exercise)uow.Exercises.Get(newExercise.Id);

                existingExercise.Title = "Modified Exercise Title";
                existingExercise.TargetMetronomeSpeed = 200;
                existingExercise.SpeedProgressWeighting = 100;
                existingExercise.TargetPracticeTime = 5000;
                existingExercise.PracticeTimeProgressWeighting = 100;

                uow.Exercises.Update(existingExercise);
                uow.Commit();

                modifiedExercise = (Exercise)uow.Exercises.Get(existingExercise.Id);

                uow.Exercises.Remove(existingExercise);
                uow.Commit();
            }

            Assert.IsNotNull(modifiedExercise);

            Assert.That(modifiedExercise.Title, Is.EqualTo("Modified Exercise Title"));
            Assert.That(modifiedExercise.TargetMetronomeSpeed, Is.EqualTo(200));
            Assert.That(modifiedExercise.DateCreated, Is.Not.Null);
            Assert.That(modifiedExercise.DateModified, Is.Not.Null);
            Assert.That(modifiedExercise.DateModified, Is.GreaterThanOrEqualTo(currentTime));
            Assert.That(modifiedExercise.SpeedProgressWeighting, Is.EqualTo(100));
            Assert.That(modifiedExercise.PracticeTimeProgressWeighting, Is.EqualTo(100));
            Assert.That(modifiedExercise.TargetPracticeTime, Is.EqualTo(5000));
        }

        [Test]
        public void ExerciseRepository_UnitOfWork_AddAndModify_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise modifiedExercise;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);
                // --- do not commit !!!

                //TODO: Try get rid of all these exercise casts using IExercise.
                Exercise existingExercise = (Exercise)uow.Exercises.Get(newExercise.Id);

                existingExercise.Title = "Modified Exercise Title";
                existingExercise.TargetMetronomeSpeed = 200;

                uow.Exercises.Update(existingExercise);
                // --- do not commit !!!
                modifiedExercise = (Exercise)uow.Exercises.Get(existingExercise.Id);

                uow.Rollback();

                ActualValueDelegate<Exercise> testDelegate = () => (Exercise)uow.Exercises.Get(modifiedExercise.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        [Test]
        public void ExerciseRepository_UnitOfWork_Delete_Operates_As_Expected()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);
                uow.Commit();
                
                uow.Exercises.Remove(newExercise);
                uow.Rollback(); // don't delete.

                // fail here if the exercise has been deleted.
                var notDeletedExercise = uow.Exercises.Get(newExercise.Id);
            }
        }

        [Test]
        public void ExerciseRepository_Deletes_A_New_Metronome_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newEx = CreateMetronomeExercise();
                uow.Exercises.Add(newEx);
                uow.Commit();

                uow.Exercises.Remove(newEx);
                uow.Commit();

                ActualValueDelegate<Exercise> testDelegate = () => (Exercise)uow.Exercises.Get(newEx.Id);
                Assert.That(testDelegate, Throws.TypeOf<DatabaseEntityNotFoundException>());
            }
        }

        [Test]
        public void ExerciseRepository_Updates_A_New_Recording_For_A_New_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise recordedExercise;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newExercise = CreateMetronomeExercise();
                
                newExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());
                uow.Exercises.Add(newExercise);
                uow.Commit();

                recordedExercise = (Exercise)uow.Exercises.Get(newExercise.Id);
            }

            Assert.AreEqual(1, recordedExercise.ExerciseActivity.Count());
            Assert.AreEqual(80, recordedExercise.ExerciseActivity[0].MetronomeSpeed);
            Assert.AreEqual(3000, recordedExercise.ExerciseActivity[0].Seconds);
            Assert.AreEqual(DateTime.Parse("2018-03-01 12:15:00"), recordedExercise.ExerciseActivity[0].StartTime);
            Assert.AreEqual(DateTime.Parse("2018-03-01 12:25:00"), recordedExercise.ExerciseActivity[0].EndTime);
        }

        [Test]
        public void ExerciseRepository_Updates_A_New_Recording_For_An_Existing_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise recordedExercise;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);

                var retrievedExercise = uow.Exercises.Get(newExercise.Id);
                retrievedExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity());

                uow.Exercises.Update(retrievedExercise);
                uow.Commit();

                recordedExercise = (Exercise)uow.Exercises.Get(retrievedExercise.Id);
            }

            Assert.AreEqual(1, recordedExercise.ExerciseActivity.Count());
            Assert.AreEqual(80, recordedExercise.ExerciseActivity[0].MetronomeSpeed);
            Assert.AreEqual(3000, recordedExercise.ExerciseActivity[0].Seconds);
            Assert.AreEqual(DateTime.Parse("2018-03-01 12:15:00"), recordedExercise.ExerciseActivity[0].StartTime);
            Assert.AreEqual(DateTime.Parse("2018-03-01 12:25:00"), recordedExercise.ExerciseActivity[0].EndTime);
        }

        [Test]
        public void ExerciseRepository_Updates_An_Existing_Recording_Only_If_Changed()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newExercise = CreateMetronomeExercise();

                newExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity(speed:80, seconds:3000, 
                    startTime: "2018-03-01 12:15:00", endTime: "2018-03-01 12:25:00"));

                newExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity(speed: 90, seconds: 4000,
                    startTime: "2018-03-02 12:15:00", endTime: "2018-03-02 12:25:00"));

                uow.Exercises.Add(newExercise);
                uow.Commit();

                var existingExercise = uow.Exercises.Get(newExercise.Id);
                var exerciseActivity = existingExercise.ExerciseActivity[0];

                exerciseActivity.MetronomeSpeed = 160;
                exerciseActivity.Seconds = 8000;

                var modifiedId = exerciseActivity.Id;

                uow.Exercises.Update(existingExercise);
                uow.Commit();

                var modifiedExercise = uow.Exercises.Get(existingExercise.Id);

                var modifiedActivity = modifiedExercise.ExerciseActivity.Where(a => a.Id == modifiedId).SingleOrDefault();
                var nonModifiedActivity = modifiedExercise.ExerciseActivity.Where(a => a.Id != modifiedId).SingleOrDefault();

                Assert.That(modifiedActivity.MetronomeSpeed, Is.EqualTo(160));
                Assert.That(modifiedActivity.Seconds, Is.EqualTo(8000));
                Assert.That(modifiedActivity.DateModified, Is.Not.Null);

                Assert.That(nonModifiedActivity.DateModified, Is.Null);
            }
        }

        [Test]
        public void ExerciseRepository_Removes_A_Deleted_Recording_For_An_Existing_Exercise_Upon_Update()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);

                var retrievedExercise = uow.Exercises.Get(newExercise.Id);

                retrievedExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity(speed: 80, seconds: 3000,
                    startTime: "2018-03-01 12:15:00", endTime: "2018-03-01 12:25:00"));

                retrievedExercise.ExerciseActivity.Add(EntityFactory.CreateExerciseActivity(speed: 90, seconds: 4000,
                    startTime: "2018-03-02 12:15:00", endTime: "2018-03-02 12:25:00"));
                 
                uow.Exercises.Update(retrievedExercise);
                uow.Commit();

                var beforeDeletionExercise = uow.Exercises.Get(retrievedExercise.Id);
                var beforeDeleteCount = beforeDeletionExercise.ExerciseActivity.Count;

                var deleteActivity = beforeDeletionExercise.ExerciseActivity[0];
                beforeDeletionExercise.ExerciseActivity.Remove(deleteActivity);

                uow.Exercises.Update(beforeDeletionExercise);
                uow.Commit();

                var afterDeletionExercise = uow.Exercises.Get(beforeDeletionExercise.Id);
                var afterDeleteCount = afterDeletionExercise.ExerciseActivity.Count;

                Assert.AreEqual(2, beforeDeleteCount);
                Assert.AreEqual(1, afterDeleteCount);
                Assert.IsNull(afterDeletionExercise.ExerciseActivity.Where(a => a.Id == deleteActivity.Id).SingleOrDefault());
            }
        }

        private Exercise CreateMetronomeExercise()
        {
            Exercise exercise = new Exercise
            {
                DateCreated = null,
                DateModified = null,
                TargetMetronomeSpeed = 150,
                TargetPracticeTime = null,
                Title = "Created Exercise Title"
            };

            return exercise;
        }
    }
}
