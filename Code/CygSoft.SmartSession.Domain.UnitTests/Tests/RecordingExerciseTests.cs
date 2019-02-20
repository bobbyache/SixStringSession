using CygSoft.SmartSession.Domain.RecordingRoutines;
using CygSoft.SmartSession.Domain.Sessions;
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
    public class RecordingExerciseTests
    {
        [Test]
        public void Disposing_Calls_Dispose_On_Recorder()
        {
            var recorder = new Mock<IRecorder>();
            var recorderMock = recorder.Object;

            using (var recordingExercise = new RecordingExercise(recorderMock)) { /* some operation */ }

            recorder.Verify(mock => mock.Dispose(), Times.Once());
        }

        [Test]
        public void Correctly_Reflects_Current_Recorder_State()
        {
            var recorder = new Mock<IRecorder>();
            var recorderMock = recorder.Object;

            recorder.Setup(mock => mock.Recording).Returns(true);
            recorder.Setup(mock => mock.Seconds).Returns(300);
            recorder.Setup(mock => mock.DisplayTime).Returns("00:05:00");

            using (var recordingExercise = new RecordingExercise(recorderMock))
            {
                Assert.That(recordingExercise.Recording, Is.EqualTo(recorderMock.Recording));
                Assert.That(recordingExercise.RecordedSeconds, Is.EqualTo(recorderMock.Seconds));
                Assert.That(recordingExercise.RecordedSecondsDisplay, Is.EqualTo(recorderMock.DisplayTime));
            }
        }

        [Test]
        public void Correctly_Changes_Recording_State_With_Recorder()
        {
            Recorder recorder = new Recorder();

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recorder.Resume();
                var isRecording = recordingExercise.Recording;

                recorder.Pause();
                var isPaused = !recordingExercise.Recording;

                recorder.Resume();
                recorder.Reset();
                var isReset = !recordingExercise.Recording;

                Assert.IsTrue(isRecording);
                Assert.IsTrue(isPaused);
                Assert.IsTrue(isReset);
            }
        }

        [Test]
        public void When_30_Sec_Add_Minute_Goes_To_Next_Exact_Minute()
        {
            var recorder = new TestRecorder(30);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.AddMinutes(1);
                Assert.AreEqual(60, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_1_Min_Add_2_Minute_Goes_To_3_Minute()
        {
            var recorder = new TestRecorder(60);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_50_Sec_Add_2_Minute_Goes_To_Next_Exact_Minute()
        {
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void Attempt_Add_Minutes_When_Recording_Does_Nothing()
        {
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.Resume();
                recordingExercise.AddMinutes(2);
                recordingExercise.Pause();

                Assert.That(recordingExercise.RecordedSeconds, Is.InRange(110, 112));
            }
        }

        [Test]
        public void When_30_Sec_Add_0_Minute_Remains_Unchanged()
        {
            var recorder = new TestRecorder(30);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.AddMinutes(0);
                Assert.AreEqual(30, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_20sec_Subtract_1_Minute_Is_0_Minutes()
        {
            var recorder = new TestRecorder(20);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.SubtractMinutes(1);
                Assert.AreEqual(0, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_2Min20sec_Subtract_1_Minute_Is_2_Minutes()
        {
            var recorder = new TestRecorder(140);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.SubtractMinutes(1);
                Assert.AreEqual(120, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_5Min40sec_Subtract_3Min_Minute_Is_3_Minutes()
        {
            var recorder = new TestRecorder(340);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.SubtractMinutes(3);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Subtracted_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            var recorder = new TestRecorder(340.3);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.SubtractMinutes(3);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Added_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            var recorder = new TestRecorder(110.3);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void Attempt_Subtract_Minutes_When_Recording_Does_Nothing()
        {
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.Resume();
                recordingExercise.SubtractMinutes(2);
                recordingExercise.Pause();

                Assert.That(recordingExercise.RecordedSeconds, Is.InRange(110, 112));
            }
        }

        [Test]
        public void TickActionCallBack_Called_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.TickActionCallBack = action;
                recordingExercise.SubtractMinutes(2);

                Assert.IsTrue(fired);
            }
        }

        [Test]
        public void TickActionCallBack_NotCalled_WhenRecording_After_Subtracting_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.TickActionCallBack = action;

                recordingExercise.Resume();
                recordingExercise.SubtractMinutes(2);
                recordingExercise.Pause();

                Assert.IsFalse(fired);
            }
        }

        [Test]
        public void TickActionCallBack_Called_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.TickActionCallBack = action;
                recordingExercise.AddMinutes(2);

                Assert.IsTrue(fired);
            }
        }

        [Test]
        public void TickActionCallBack_NotCalled_WhenRecording_After_Adding_Minutes()
        {
            bool fired = false;
            Action action = () => fired = true;
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.TickActionCallBack = action;

                recordingExercise.Resume();
                recordingExercise.AddMinutes(2);
                recordingExercise.Pause();

                Assert.IsFalse(fired);
            }
        }

        [Test]
        public void RecordingStatusChanged_Event_Fired_On_Resume()
        {
            var called = false;
            var recorder = new TestRecorder(110);

            using (var recordingExercise = new RecordingExercise(recorder))
            {
                recordingExercise.RecordingStatusChanged += (sender, args) => called = true;

                recordingExercise.Resume();

                Assert.IsTrue(called);
            }
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
