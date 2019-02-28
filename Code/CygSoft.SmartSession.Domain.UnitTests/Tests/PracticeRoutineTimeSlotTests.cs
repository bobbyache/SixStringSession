using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class PracticeRoutineTimeSlotTests
    {
        [Test]
        public void PracticeRoutineTimeSlot_New_Constructor_Should_Not_Take_Null_ExerciseList()
        {
            ActualValueDelegate<object> nullConstructor = () => new PracticeRoutineTimeSlot("Title", 120, null);
            Assert.That(nullConstructor, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutineTimeSlot_Existing_Constructor_Should_Not_Take_Null_ExerciseList()
        {
            ActualValueDelegate<object> nullConstructor = () => new PracticeRoutineTimeSlot(1, "Title", 120, null);
            Assert.That(nullConstructor, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutineTimeSlot_Initialize_As_New_Has_CorrectState()
        {
            PracticeRoutineTimeSlot timeSlot = new PracticeRoutineTimeSlot("Title", 120, EntityFactory.CreateSingleExerciseList());
            Assert.AreEqual("Title", timeSlot.Title);
            Assert.AreEqual(120, timeSlot.AssignedSeconds);
            Assert.AreEqual(1, timeSlot.Exercises.Count());
            Assert.AreEqual(0, timeSlot.Id);
            Assert.IsNull(timeSlot.DateCreated);
            Assert.IsNull(timeSlot.DateModified);
        }

        [Test]
        public void PracticeRoutineTimeSlot_Initialize_As_Existing_Has_CorrectState()
        {
            PracticeRoutineTimeSlot timeSlot = new PracticeRoutineTimeSlot(1, "Title", 120, EntityFactory.CreateSingleExerciseList());
            Assert.AreEqual("Title", timeSlot.Title);
            Assert.AreEqual(120, timeSlot.AssignedSeconds);
            Assert.AreEqual(1, timeSlot.Exercises.Count());
            Assert.AreEqual(1, timeSlot.Id);
            Assert.IsNull(timeSlot.DateCreated);
            Assert.IsNull(timeSlot.DateModified);
        }
    }
}
