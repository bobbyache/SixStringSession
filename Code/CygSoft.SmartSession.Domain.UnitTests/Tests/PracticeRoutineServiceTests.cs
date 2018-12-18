using CygSoft.SmartSession.Domain.PracticeRoutines;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class PracticeRoutineServiceTests
    {
        [Test]
        public void PracticeRoutineService_CreateNew_Creates_Exercise_With_Proper_Initialisation_State()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var practiceRoutineService = new PracticeRoutineService(unitOfWork.Object);

            var exercise = practiceRoutineService.Create();

            Assert.That(exercise, Is.Not.Null);
            Assert.IsTrue(exercise.Title.StartsWith("New Practice Routine - "));
        }

        // You can't do anything else here unless you inject the repositories into UnitOfWork.
    }
}
