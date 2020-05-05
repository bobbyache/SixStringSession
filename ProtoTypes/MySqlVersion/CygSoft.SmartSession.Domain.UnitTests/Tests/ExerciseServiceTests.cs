using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using Moq;
using NUnit.Framework;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseServiceTests
    {
        [Test]
        public void ExerciseService_CreateNew_Creates_Exercise_With_Proper_Initialisation_State()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var exerciseService = new ExerciseService(unitOfWork.Object);

            var exercise = exerciseService.Create();

            Assert.That(exercise, Is.Not.Null);
            Assert.IsTrue(exercise.Title.StartsWith("New Exercise Item - "));
        }
    }
}
