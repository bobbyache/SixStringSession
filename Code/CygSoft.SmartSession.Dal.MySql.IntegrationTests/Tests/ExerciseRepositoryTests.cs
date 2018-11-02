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
                PercentageCompleteCalculationType = PercentCompleteCalculationStrategy.MetronomeSpeed
            };

            return exercise;
        }
    }
}
