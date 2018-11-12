using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
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
        public void ExerciseRepository_Creates_A_New_Metronome_Exercise_Successfully()
        {
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
            Assert.That(ex1.InitialMetronomeSpeed, Is.EqualTo(50));
            Assert.That(ex1.TargetMetronomeSpeed, Is.EqualTo(150));
            Assert.That(ex1.TargetPracticeTime, Is.Null);
            Assert.That(ex1.PercentageCompleteCalculationType, Is.EqualTo(PercentCompleteCalculationStrategy.MetronomeSpeed));
            Assert.That(ex1.PracticalityRating, Is.EqualTo(2));
            Assert.That(ex1.DifficultyRating, Is.EqualTo(3));
            Assert.That(ex1.DateCreated, Is.Not.Null);
            Assert.That(ex1.DateModified, Is.Null);

            Assert.Fail("Must implement proper search tests...");
        }

        [Test]
        public void ExerciseRepository_Updates_A_Metronome_Exercise_Successfully()
        {
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
                existingExercise.InitialMetronomeSpeed = 20;
                existingExercise.TargetMetronomeSpeed = 200;

                uow.Exercises.Update(existingExercise);
                uow.Commit();

                modifiedExercise = uow.Exercises.Get(existingExercise.Id);

                uow.Exercises.Remove(existingExercise);
            }

            Assert.IsNotNull(modifiedExercise);

            Assert.That(modifiedExercise.Title, Is.EqualTo("Modified Exercise Title"));
            Assert.That(modifiedExercise.InitialMetronomeSpeed, Is.EqualTo(20));
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
                existingExercise.InitialMetronomeSpeed = 20;
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

        private Exercise CreateMetronomeExercise()
        {
            Exercise exercise = new Exercise
            {
                DateCreated = DateTime.Parse("2015-10-30 01:02:03"),
                DateModified = DateTime.Parse("2015-11-20 13:50:59"),
                InitialMetronomeSpeed = 50,
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
