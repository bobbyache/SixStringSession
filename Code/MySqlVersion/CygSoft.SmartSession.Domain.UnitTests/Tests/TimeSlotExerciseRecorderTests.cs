using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class TimeSlotExerciseRecorderTests
    {
        [Test]
        public void TimeSlotTimer_RemainingTime_Works_As_Expected()
        {
            int recordedTime = 300;
            int assignedTime = 600;

            var recorder = new TestRecorder(recordedTime);
            var manualProgress = new ManualProgress(0, 50);
            var speedProgress = new SpeedProgress(0, 0, 120, 50);
            var practiceTimeProgress = new PracticeTimeProgress(0, 600, 50);

            var exerciseRecorder = new TimeSlotExerciseRecorder(recorder, 1, "Exercise Title", speedProgress, practiceTimeProgress, manualProgress, assignedTime);

            Assert.That(exerciseRecorder.RemainingSeconds, Is.EqualTo(300));
        }

        [Test]
        public void TimeSlotTimer_RemainingTime_Negative_Returns_0()
        {
            int recordedTime = 601;
            int assignedTime = 600;

            var recorder = new TestRecorder(recordedTime);
            var manualProgress = new ManualProgress(0, 50);
            var speedProgress = new SpeedProgress(0, 0, 120, 50);
            var practiceTimeProgress = new PracticeTimeProgress(0, 600, 50);

            var exerciseRecorder = new TimeSlotExerciseRecorder(recorder, 1, "Exercise Title", speedProgress, practiceTimeProgress, manualProgress, assignedTime);

            Assert.That(exerciseRecorder.RemainingSeconds, Is.EqualTo(0));
        }

        public class TestRecorder : Recorder
        {
            public TestRecorder(double initialSeconds) : base()
            {
                base.recordedSeconds = initialSeconds;
            }
        }
    }
}
