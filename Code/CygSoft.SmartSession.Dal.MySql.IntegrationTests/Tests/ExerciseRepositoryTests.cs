using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure.Enums;
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
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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

        [Test]
        public void ExerciseRepository_Find_Exercise_With_Specific_CalcPercentType_Gets_Applicable_Recs()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

            //using (var uow = new UnitOfWork(Settings.AppConnectionString))
            //{
            //    IExerciseSearchCriteria crit = new ExerciseSearchCriteria();
            //    crit.PercentageCompleteCalculationType = Metronome

            //    var exercises = uow.Exercises.Find(crit);
            //    Assert.That(exercises.Where(ex => ex.Title == "Orange Exercise").SingleOrDefault(), Is.Not.Null);
            //    Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 1").SingleOrDefault(), Is.Not.Null);
            //    Assert.That(exercises.Where(ex => ex.Title == "Green Exercise 2").SingleOrDefault(), Is.Not.Null);
            //}

            throw new NotImplementedException();
        }

        [Test]
        public void ExerciseRepository_Find_Exercise_With_DateModified_Before_ToDateModified()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);
            
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
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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
        public void ExerciseRepository_Find_Exercise_With_DateModified_On_Or_After_ToDateModified()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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
            Funcs.RunScript("exercise-find-tests.sql", Settings.AppConnectionString);

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
        public void ExerciseRepository_Creates_A_New_Metronome_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise ex1;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newEx = CreateMetronomeExercise();
                uow.Exercises.Add(newEx);
                uow.Commit();

                ex1 = uow.Exercises.Get(newEx.Id);

                uow.Exercises.Remove(newEx);
                uow.Commit();
            }

            Assert.IsNotNull(ex1);

            Assert.That(ex1.Title, Is.EqualTo("Created Exercise Title"));
            Assert.That(ex1.TargetMetronomeSpeed, Is.EqualTo(150));
            Assert.That(ex1.TargetPracticeTime, Is.Null);
            Assert.That(ex1.PercentageCompleteCalculationType, Is.EqualTo(PercentCompleteCalculationStrategy.MetronomeSpeed));
            Assert.That(ex1.PracticalityRating, Is.EqualTo(2));
            Assert.That(ex1.DifficultyRating, Is.EqualTo(3));
            Assert.That(ex1.DateCreated, Is.Not.Null);
            Assert.That(ex1.DateModified, Is.Null);
        }

        [Test]
        public void ExerciseRepository_Updates_A_Metronome_Exercise_Successfully()
        {
            Funcs.RunScript("delete-all-records.sql", Settings.AppConnectionString);

            Exercise modifiedExercise;
            var currentTime = Funcs.RemoveMilliSeconds(DateTime.Now);

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newExercise = CreateMetronomeExercise();
                uow.Exercises.Add(newExercise);
                uow.Commit();

                Exercise existingExercise = uow.Exercises.Get(newExercise.Id);

                existingExercise.Title = "Modified Exercise Title";
                existingExercise.PracticalityRating = 5;
                existingExercise.DifficultyRating = 5;
                existingExercise.TargetMetronomeSpeed = 200;

                uow.Exercises.Update(existingExercise);
                uow.Commit();

                modifiedExercise = uow.Exercises.Get(existingExercise.Id);

                uow.Exercises.Remove(existingExercise);
                uow.Commit();
            }

            Assert.IsNotNull(modifiedExercise);

            Assert.That(modifiedExercise.Title, Is.EqualTo("Modified Exercise Title"));
            Assert.That(modifiedExercise.TargetMetronomeSpeed, Is.EqualTo(200));
            Assert.That(modifiedExercise.TargetPracticeTime, Is.Null);
            Assert.That(modifiedExercise.PercentageCompleteCalculationType, Is.EqualTo(PercentCompleteCalculationStrategy.MetronomeSpeed));
            Assert.That(modifiedExercise.PracticalityRating, Is.EqualTo(5));
            Assert.That(modifiedExercise.DifficultyRating, Is.EqualTo(5));
            Assert.That(modifiedExercise.DateCreated, Is.Not.Null);
            Assert.That(modifiedExercise.DateModified, Is.Not.Null);
            Assert.That(modifiedExercise.DateModified, Is.GreaterThanOrEqualTo(currentTime));
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

                Exercise existingExercise = uow.Exercises.Get(newExercise.Id);

                existingExercise.Title = "Modified Exercise Title";
                existingExercise.PracticalityRating = 5;
                existingExercise.DifficultyRating = 5;
                existingExercise.TargetMetronomeSpeed = 200;

                uow.Exercises.Update(existingExercise);
                // --- do not commit !!!
                modifiedExercise = uow.Exercises.Get(existingExercise.Id);

                uow.Rollback();

                ActualValueDelegate<Exercise> testDelegate = () => uow.Exercises.Get(modifiedExercise.Id);
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

                ActualValueDelegate<Exercise> testDelegate = () => uow.Exercises.Get(newEx.Id);
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
                newExercise.AddRecording(80, 3000, DateTime.Parse("2018-03-01 12:15:00"), DateTime.Parse("2018-03-01 12:25:00"));
                uow.Exercises.Add(newExercise);
                uow.Commit();

                recordedExercise = uow.Exercises.Get(newExercise.Id);
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
                retrievedExercise.AddRecording(80, 3000, DateTime.Parse("2018-03-01 12:15:00"), DateTime.Parse("2018-03-01 12:25:00"));

                uow.Exercises.Update(retrievedExercise);
                uow.Commit();

                recordedExercise = uow.Exercises.Get(retrievedExercise.Id);
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
                newExercise.AddRecording(80, 3000, DateTime.Parse("2018-03-01 12:15:00"), DateTime.Parse("2018-03-01 12:25:00"));
                newExercise.AddRecording(90, 4000, DateTime.Parse("2018-03-02 12:15:00"), DateTime.Parse("2018-03-02 12:25:00"));
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
                retrievedExercise.AddRecording(80, 3000, DateTime.Parse("2018-03-01 12:15:00"), DateTime.Parse("2018-03-01 12:25:00"));
                retrievedExercise.AddRecording(90, 4000, DateTime.Parse("2018-03-02 12:15:00"), DateTime.Parse("2018-03-02 12:25:00"));
                 
                uow.Exercises.Update(retrievedExercise);
                uow.Commit();

                var beforeDeletionExercise = uow.Exercises.Get(retrievedExercise.Id);
                var deleteActivityId = beforeDeletionExercise.ExerciseActivity[0].Id;
                var beforeDeleteCount = beforeDeletionExercise.ExerciseActivity.Count;

                beforeDeletionExercise.RemoveRecording(deleteActivityId);

                uow.Exercises.Update(beforeDeletionExercise);
                uow.Commit();

                var afterDeletionExercise = uow.Exercises.Get(beforeDeletionExercise.Id);
                var afterDeleteCount = afterDeletionExercise.ExerciseActivity.Count;

                Assert.AreEqual(2, beforeDeleteCount);
                Assert.AreEqual(1, afterDeleteCount);
                Assert.IsNull(afterDeletionExercise.ExerciseActivity.Where(a => a.Id == deleteActivityId).SingleOrDefault());
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
                PracticalityRating = 2,
                DifficultyRating = 3,
                Title = "Created Exercise Title",
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed
            };

            return exercise;
        }
    }
}
