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
        //[Test]
        //public void TimeSlotExerciseRecorder_Instantiate_With_Null_Timer_Throws_Exception()
        //{
        //    var recorder = new Recorder();
        //    var manualProgress = new ManualProgress(0, 50);
        //    var speedProgress = new SpeedProgress(0, 0, 120, 50);
        //    var practiceTimeProgress = new PracticeTimeProgress(0, 600, 50);

        //    ActualValueDelegate<object> constructor = () => new TimeSlotExerciseRecorder(recorder, 1, "Exercise Title", 
        //        speedProgress, practiceTimeProgress, manualProgress, null);

        //    Assert.That(constructor, Throws.TypeOf<ArgumentNullException>());
        //}

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

            Assert.That(exerciseRecorder.RemainingTime, Is.EqualTo(300));
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

            Assert.That(exerciseRecorder.RemainingTime, Is.EqualTo(0));
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
