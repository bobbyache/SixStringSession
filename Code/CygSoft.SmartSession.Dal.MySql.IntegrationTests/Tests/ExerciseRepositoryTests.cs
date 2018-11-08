using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using NUnit.Framework;
using System;
using System.Linq;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class ExerciseRepositoryTests
    {
        

        [Test]
        public void ExerciseRepository_Finds_Existing_2_Exercises()
        {
            Exercise ex1;
            Exercise ex2;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                var exercises = uow.Exercises.Find(null);

                ex1 = exercises.Where(e => e.Title == "Test Title 1").SingleOrDefault();
                ex2 = exercises.Where(e => e.Title == "Test Title 2").SingleOrDefault();
            }

            Assert.IsNotNull(ex1);
            Assert.IsNotNull(ex2);

            Assert.That(ex1.InitialMetronomeSpeed, Is.Null);
            Assert.That(ex1.TargetMetronomeSpeed, Is.EqualTo(80));
            Assert.That(ex1.TargetPracticeTime, Is.EqualTo(3600));
            Assert.That(ex1.PercentageCompleteCalculationType, Is.EqualTo(PercentCompleteCalculationStrategy.PracticeTime));
            Assert.That(ex1.PracticalityRating, Is.EqualTo(3));
            Assert.That(ex1.DifficultyRating, Is.EqualTo(1));
            Assert.That(ex1.DateCreated, Is.EqualTo(DateTime.Parse("2015-10-30 01:02:03")));
            Assert.That(ex1.DateModified, Is.EqualTo(DateTime.Parse("2015-11-20 13:50:59")));
        }

        [Test]
        public void ExerciseRepository_Creates_New_Metronome_Exercise()
        {
            Exercise ex1;

            using (var uow = new UnitOfWork(Settings.AppConnectionString))
            {
                Exercise newEx = CreateMetronomeExercise();
                uow.Exercises.Add(newEx);
                uow.Commit();

                ex1 = uow.Exercises.Get(newEx.Id);
            }

            Assert.IsNotNull(ex1);

            Assert.That(ex1.InitialMetronomeSpeed, Is.EqualTo(50));
            Assert.That(ex1.TargetMetronomeSpeed, Is.EqualTo(150));
            Assert.That(ex1.TargetPracticeTime, Is.Null);
            Assert.That(ex1.PercentageCompleteCalculationType, Is.EqualTo(PercentCompleteCalculationStrategy.MetronomeSpeed));
            Assert.That(ex1.PracticalityRating, Is.EqualTo(2));
            Assert.That(ex1.DifficultyRating, Is.EqualTo(3));
            //Assert.That(ex1.DateCreated, Is.EqualTo(DateTime.Parse("2015-10-30 01:02:03")));
            //Assert.That(ex1.DateModified, Is.EqualTo(DateTime.Parse("2015-11-20 13:50:59")));
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
