using CygSoft.SmartSession.Domain.Recording;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class RecorderTests
    {
        [Test]
        public void Recorder_When_30_Sec_Add_Minute_Goes_To_Next_Exact_Minute()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(30);
            exerciseRecorder.AddMinutes(1);
            Assert.AreEqual(60, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_1_Min_Add_2_Minute_Goes_To_3_Minute()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(60);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_50_Sec_Add_2_Minute_Goes_To_Next_Exact_Minute()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_Attempt_Add_Minutes_When_Recording_Does_Nothing()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.Resume();
            exerciseRecorder.AddMinutes(2);
            exerciseRecorder.Pause();

            Assert.That(exerciseRecorder.PreciseSeconds, Is.InRange(110, 112));
        }

        [Test]
        public void Recorder_When_30_Sec_Add_0_Minute_Remains_Unchanged()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(30);
            exerciseRecorder.AddMinutes(0);
            Assert.AreEqual(30, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_Is_20sec_Subtract_1_Minute_Is_0_Minutes()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(20);
            exerciseRecorder.SubtractMinutes(1);
            Assert.AreEqual(0, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_Is_2Min20sec_Subtract_1_Minute_Is_2_Minutes()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(140);
            exerciseRecorder.SubtractMinutes(1);
            Assert.AreEqual(120, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_Is_5Min40sec_Subtract_3Min_Minute_Is_3_Minutes()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(340);
            exerciseRecorder.SubtractMinutes(3);
            Assert.AreEqual(180, exerciseRecorder.PreciseSeconds);
        }


        [Test]
        public void Recorder_When_Subtracted_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(340.3);
            exerciseRecorder.SubtractMinutes(3);
            Assert.AreEqual(180, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_When_Added_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(110.3);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.PreciseSeconds);
        }

        [Test]
        public void Recorder_Attempt_Subtract_Minutes_When_Recording_Does_Nothing()
        {
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.Resume();
            exerciseRecorder.SubtractMinutes(2);
            exerciseRecorder.Pause();

            Assert.That(exerciseRecorder.PreciseSeconds, Is.InRange(110, 112));
        }

        [Test]
        public void Recorder_TickActionCallBack_Called_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.SubtractMinutes(2);

            Assert.IsTrue(fired);
        }

        [Test]
        public void Recorder_TickActionCallBack_NotCalled_WhenRecording_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.Resume();
            exerciseRecorder.SubtractMinutes(2);
            exerciseRecorder.Pause();

            Assert.IsFalse(fired);
        }

        [Test]
        public void Recorder_TickActionCallBack_Called_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.AddMinutes(2);

            Assert.IsTrue(fired);
        }

        [Test]
        public void Recorder_TickActionCallBack_NotCalled_WhenRecording_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IRecorder exerciseRecorder = new TestExerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.Resume();
            exerciseRecorder.AddMinutes(2);
            exerciseRecorder.Pause();

            Assert.IsFalse(fired);
        }

        public class TestExerciseRecorder : Recorder
        {
            public TestExerciseRecorder(double initialSeconds) : base()
            {
                base.recordedSeconds = initialSeconds;
            }
        }
    }
}
