using CygSoft.SmartSession.Domain.Sessions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseRecorderTests
    {
        [Test]
        public void ExerciseRecorder_When_30_Sec_Add_Minute_Goes_To_Next_Exact_Minute()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(30);
            exerciseRecorder.AddMinutes(1);
            Assert.AreEqual(60, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_1_Min_Add_2_Minute_Goes_To_3_Minute()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(60);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_50_Sec_Add_2_Minute_Goes_To_Next_Exact_Minute()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_Attempt_Add_Minutes_When_Recording_Does_Nothing()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.Resume();
            exerciseRecorder.AddMinutes(2);
            exerciseRecorder.Pause();

            Assert.That(exerciseRecorder.Seconds, Is.InRange(110, 112));
        }

        [Test]
        public void ExerciseRecorder_When_30_Sec_Add_0_Minute_Remains_Unchanged()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(30);
            exerciseRecorder.AddMinutes(0);
            Assert.AreEqual(30, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_Is_20sec_Subtract_1_Minute_Is_0_Minutes()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(20);
            exerciseRecorder.SubtractMinutes(1);
            Assert.AreEqual(0, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_Is_2Min20sec_Subtract_1_Minute_Is_2_Minutes()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(140);
            exerciseRecorder.SubtractMinutes(1);
            Assert.AreEqual(120, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_Is_5Min40sec_Subtract_3Min_Minute_Is_3_Minutes()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(340);
            exerciseRecorder.SubtractMinutes(3);
            Assert.AreEqual(180, exerciseRecorder.Seconds);
        }


        [Test]
        public void ExerciseRecorder_When_Subtracted_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(340.3);
            exerciseRecorder.SubtractMinutes(3);
            Assert.AreEqual(180, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_When_Added_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110.3);
            exerciseRecorder.AddMinutes(2);
            Assert.AreEqual(180, exerciseRecorder.Seconds);
        }

        [Test]
        public void ExerciseRecorder_Attempt_Subtract_Minutes_When_Recording_Does_Nothing()
        {
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.Resume();
            exerciseRecorder.SubtractMinutes(2);
            exerciseRecorder.Pause();

            Assert.That(exerciseRecorder.Seconds, Is.InRange(110, 112));
        }

        [Test]
        public void ExerciseRecorder_TickActionCallBack_Called_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.SubtractMinutes(2);

            Assert.IsTrue(fired);
        }

        [Test]
        public void ExerciseRecorder_TickActionCallBack_NotCalled_WhenRecording_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.Resume();
            exerciseRecorder.SubtractMinutes(2);
            exerciseRecorder.Pause();

            Assert.IsFalse(fired);
        }

        [Test]
        public void ExerciseRecorder_TickActionCallBack_Called_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.AddMinutes(2);

            Assert.IsTrue(fired);
        }

        [Test]
        public void ExerciseRecorder_TickActionCallBack_NotCalled_WhenRecording_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            IExerciseRecorder exerciseRecorder = new TestExcerciseRecorder(110);
            exerciseRecorder.TickActionCallBack = action;

            exerciseRecorder.Resume();
            exerciseRecorder.AddMinutes(2);
            exerciseRecorder.Pause();

            Assert.IsFalse(fired);
        }

        public class TestExcerciseRecorder : ExerciseRecorder
        {
            public TestExcerciseRecorder(double initialSeconds) : base()
            {
                base.recordedSeconds = initialSeconds;
            }
        }
    }
}
