using CygSoft.SmartSession.Dal.MySql.IntegrationTests.Helpers;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Dal.MySql.IntegrationTests.Tests
{
    [TestFixture]
    public class ExerciseRepositoryTests
    {
        [Test]
        public void TestIt()
        {
            // https://github.com/timschreiber/DapperUnitOfWork/blob/master/DapperUnitOfWork.Console/Program.cs

            using (var uow = new UnitOfWork(Settings.ConnectionString))
            {
                uow.Exercises.Add(CreateExercise());
                uow.Complete(); // should be commit
            }

            Assert.Fail("Boilerplate has been set up. Now to start using it.");
        }

        private Exercise CreateExercise()
        {
            Exercise exercise = new Exercise
            {
                Id = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                InitialMetronomeSpeed = null,
                TargetMetronomeSpeed = 150,
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed,

                ExerciseActivity = new List<SessionExerciseActivity>
                {
                    new SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/02"),
                        DateModified = DateTime.Parse("2018/01/02"),
                        StartMetronomeSpeed = 40,
                        ComfortMetronomeSpeed = 50,
                        AchievedMetronomeSpeed = 80,
                        StartTime = DateTime.Parse("2018/01/02 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/02 10:15:00"),
                        ExerciseId = 1 },

                    new SessionExerciseActivity {
                        Id = 1,
                        DateCreated = DateTime.Parse("2018/01/03"),
                        DateModified = DateTime.Parse("2018/01/03"),
                        StartMetronomeSpeed = 50,
                        ComfortMetronomeSpeed = 100,
                        AchievedMetronomeSpeed = 110,
                        StartTime = DateTime.Parse("2018/01/03 10:12:00"),
                        EndTime = DateTime.Parse("2018/01/03 10:15:00"),
                        ExerciseId = 1 }
                }
                return exercise;
            };
        }
    }
}
