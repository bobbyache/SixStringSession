using CygSoft.SmartSession.Domain.Recording;
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
    public class PracticeRoutineRecorderTests
    {
        [Test]
        public void PracticeRoutineRecorder_When_Initialized_Reflects_Correct_State()
        {
            var exercises = new List<ITimeSlotExerciseRecorder> { };

            var existingRoutine = new PracticeRoutineRecorder(2, "Existing Routine", exercises);
            var newRoutine = new PracticeRoutineRecorder("New Routine", exercises);

            Assert.AreEqual(2, existingRoutine.Id);
            Assert.AreEqual("Existing Routine", existingRoutine.Title);

            Assert.AreEqual(0, newRoutine.Id);
            Assert.AreEqual("New Routine", newRoutine.Title);
        }

        [Test]
        public void PracticeRoutineRecorder_Reflects_Total_Recorded_Seconds_Correctly()
        {
            var ex1 = new Mock<ITimeSlotExerciseRecorder>();
            ex1.Setup(obj => obj.RecordedSeconds).Returns(300);
            var ex2 = new Mock<ITimeSlotExerciseRecorder>();
            ex2.Setup(obj => obj.RecordedSeconds).Returns(300);
            var ex3 = new Mock<ITimeSlotExerciseRecorder>();
            ex3.Setup(obj => obj.RecordedSeconds).Returns(300);

            var exercises = new List<ITimeSlotExerciseRecorder>
            {
                ex1.Object,
                ex2.Object,
                ex3.Object
            };

            PracticeRoutineRecorder practiceRoutineRecorder = new PracticeRoutineRecorder("Recording Routine", exercises);
            Assert.That(practiceRoutineRecorder.RecordedSeconds, Is.EqualTo(900));
            Assert.That(practiceRoutineRecorder.RecordedSecondsDisplay, Is.EqualTo("00:15:00"));
        }
    }
}
