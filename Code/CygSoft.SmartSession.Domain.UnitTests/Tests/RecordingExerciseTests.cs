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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorderMock, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object)) { /* some operation */ }

            recorder.Verify(mock => mock.Dispose(), Times.Once());
        }

        [Test]
        public void Correctly_Reflects_Current_Recorder_State()
        {
            var recorder = new Mock<IRecorder>();
            var recorderMock = recorder.Object;
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            recorder.Setup(mock => mock.Recording).Returns(true);
            recorder.Setup(mock => mock.PreciseSeconds).Returns(300);
            recorder.Setup(mock => mock.DisplayTime).Returns("00:05:00");

            using (var recordingExercise = new RecordingExercise(recorderMock, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                Assert.That(recordingExercise.Recording, Is.EqualTo(recorderMock.Recording));
                Assert.That(recordingExercise.RecordedSeconds, Is.EqualTo(recorderMock.PreciseSeconds));
                Assert.That(recordingExercise.RecordedSecondsDisplay, Is.EqualTo(recorderMock.DisplayTime));
            }
        }

        [Test]
        public void Correctly_Changes_Recording_State_With_Recorder()
        {
            Recorder recorder = new Recorder();
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.AddMinutes(1);
                Assert.AreEqual(60, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_1_Min_Add_2_Minute_Goes_To_3_Minute()
        {
            var recorder = new TestRecorder(60);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_50_Sec_Add_2_Minute_Goes_To_Next_Exact_Minute()
        {
            var recorder = new TestRecorder(110);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void Attempt_Add_Minutes_When_Recording_Does_Nothing()
        {
            var recorder = new TestRecorder(110);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.AddMinutes(0);
                Assert.AreEqual(30, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_20sec_Subtract_1_Minute_Is_0_Minutes()
        {
            var recorder = new TestRecorder(20);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.SubtractMinutes(1);
                Assert.AreEqual(0, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_2Min20sec_Subtract_1_Minute_Is_2_Minutes()
        {
            var recorder = new TestRecorder(140);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.SubtractMinutes(1);
                Assert.AreEqual(120, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Is_5Min40sec_Subtract_3Min_Minute_Is_3_Minutes()
        {
            var recorder = new TestRecorder(340);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.SubtractMinutes(3);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Subtracted_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            var recorder = new TestRecorder(340.3);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.SubtractMinutes(3);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void When_Added_And_SecondsAreFraction_Removes_Fraction_InIncrement()
        {
            var recorder = new TestRecorder(110.3);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.AddMinutes(2);
                Assert.AreEqual(180, recordingExercise.RecordedSeconds);
            }
        }

        [Test]
        public void Attempt_Subtract_Minutes_When_Recording_Does_Nothing()
        {
            var recorder = new TestRecorder(110);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
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
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.RecordingStatusChanged += (sender, args) => called = true;

                recordingExercise.Resume();

                Assert.IsTrue(called);
            }
        }

        [Test]
        public void RecordingStatusChanged_Event_Fired_On_Pause()
        {
            var called = false;
            var recorder = new TestRecorder(110);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.Resume();
                recordingExercise.RecordingStatusChanged += (sender, args) => called = true;
                recordingExercise.Pause();

                Assert.IsTrue(called);
            }
        }

        [Test]
        public void RecordingStatusChanged_Event_Fired_On_Reset()
        {
            var called = false;
            var recorder = new TestRecorder(110);
            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.Resume();
                recordingExercise.Pause();
                recordingExercise.RecordingStatusChanged += (sender, args) => called = true;
                recordingExercise.Reset();

                Assert.IsTrue(called);
            }
        }

        [Test]
        public void Exercise_SpeedProgress_Properties_Are_Reflected_Correctly_On_Initialization()
        {
            var recorder = new Mock<IRecorder>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            var manualProgress = new Mock<IManualProgress>();

            var speedProgress = new Mock<ISpeedProgress>();
            speedProgress.Setup(obj => obj.CurrentSpeed).Returns(60);
            speedProgress.Setup(obj => obj.CalculateProgress()).Returns(50);

            using (var recordingExercise = new RecordingExercise(recorder.Object, "Exercise Title",
                 speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object
             ))
            {
                Assert.That(recordingExercise.Title, Is.EqualTo("Exercise Title"));
                Assert.That(recordingExercise.CurrentSpeed, Is.EqualTo(60));
                Assert.That(recordingExercise.CurrentSpeedProgress, Is.EqualTo(50));
            }
        }

        [Test]
        public void Exercise_PracticeTimeProgress_Properties_Are_Reflected_Correctly_On_Initialization()
        {
            var recorder = new TestRecorder(0);
            var manualProgress = new Mock<IManualProgress>();
            var speedProgress = new Mock<ISpeedProgress>();

            var practiceTimeProgress = new PracticeTimeProgress(300, 600, 100);

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title",
                 speedProgress.Object, practiceTimeProgress, manualProgress.Object
             ))
            {
                Assert.That(recordingExercise.Title, Is.EqualTo("Exercise Title"));
                Assert.That(recordingExercise.CurrentTotalSeconds, Is.EqualTo(300));
                Assert.That(recordingExercise.CurrentTimeProgress, Is.EqualTo(50));
            }
        }

        [Test]
        public void Exercise_ManualProgress_Properties_Are_Reflected_Correctly_On_Initialization()
        {
            var recorder = new Mock<IRecorder>();

            var speedProgress = new Mock<ISpeedProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();

            var manualProgress = new Mock<IManualProgress>();
            manualProgress.Setup(obj => obj.CalculateProgress()).Returns(50);

            using (var recordingExercise = new RecordingExercise(recorder.Object, "Exercise Title",
                 speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object
             ))
            {
                Assert.That(recordingExercise.Title, Is.EqualTo("Exercise Title"));
                Assert.That(recordingExercise.CurrentManualProgress, Is.EqualTo(50));
            }
        }

        [Test]
        public void Displays_Correct_TotalSecondsDisplay()
        {
            var recorder = new TestRecorder(300);
            var manualProgress = new Mock<IManualProgress>();
            var speedProgress = new Mock<ISpeedProgress>();

            var practiceTimeProgress = new PracticeTimeProgress(600, 1000, 100);

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress, manualProgress.Object))
            {
                Assert.That(recordingExercise.TotalSecondsDisplay, Is.EqualTo("00:15:00"));
                Assert.That(recordingExercise.RecordedSecondsDisplay, Is.EqualTo("00:05:00"));
            }
        }

        [Test]
        public void WeightedProgressCalculator_CalculateTotalProgress_Test_1()
        {
            var recorder = new TestRecorder(300);

            var speedProgress = new Mock<ISpeedProgress>();
            speedProgress.Setup(obj => obj.CalculateProgress()).Returns(25);
            speedProgress.Setup(obj => obj.Weighting).Returns(6000);

            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();
            practiceTimeProgress.Setup(obj => obj.CalculateProgress()).Returns(100);
            practiceTimeProgress.Setup(obj => obj.Weighting).Returns(6000);

            var manualProgress = new Mock<IManualProgress>();
            manualProgress.Setup(obj => obj.CalculateProgress()).Returns(60);
            manualProgress.Setup(obj => obj.Weighting).Returns(12000);

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress.Object, manualProgress.Object))
            {
                Assert.That(recordingExercise.CurrentOverAllProgress, Is.EqualTo(61.25));
            }
        }

        [Test]
        public void When_Time_Recorded_CurrentTotalSeconds_Reflects_TimeRecorded_And_Previous_PracticeTime()
        {
            var speedProgress = new Mock<ISpeedProgress>();
            var manualProgress = new Mock<IManualProgress>();

            var recorder = new TestRecorder(300);
            var practiceTimeProgress = new PracticeTimeProgress(300, 900, 100);

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress, manualProgress.Object))
            {
                Assert.AreEqual(600, recordingExercise.CurrentTotalSeconds);
                Assert.That(recordingExercise.CurrentTimeProgress, Is.InRange(66.6, 66.7));
            }
        }


        [Test]
        public void When_Time_Recorded_And_Time_Added_CurrentTotalSeconds_Reflects_TimeRecorded_And_Previous_PracticeTime()
        {
            var speedProgress = new Mock<ISpeedProgress>();
            var manualProgress = new Mock<IManualProgress>();

            var recorder = new TestRecorder(300);
            var practiceTimeProgress = new PracticeTimeProgress(300, 1200, 100);

            using (var recordingExercise = new RecordingExercise(recorder, "Exercise Title", speedProgress.Object, practiceTimeProgress, manualProgress.Object))
            {
                recordingExercise.AddSeconds(300);

                Assert.AreEqual(900, recordingExercise.CurrentTotalSeconds);
                Assert.That(recordingExercise.CurrentTimeProgress, Is.EqualTo(75));
            }
        }

        [Test]
        public void When_SpeedRecorded_Changes_Current_Speed_Positively_And_SpeedProgress_Reflects_This_Correctly()
        {
            var recorder = new Mock<IRecorder>();
            var manualProgress = new Mock<IManualProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();

            var speedProgress = new SpeedProgress(50, 100, 150, 100);

            using (var recordingExercise = new RecordingExercise(recorder.Object, "Exercise Title", speedProgress, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.IncrementSpeed(25);

                Assert.AreEqual(125, recordingExercise.CurrentSpeed);
                Assert.That(recordingExercise.CurrentSpeedProgress, Is.EqualTo(75));
            }
        }

        [Test]
        public void When_SpeedRecorded_Changes_Current_Speed_Negatively_And_SpeedProgress_Reflects_This_Correctly()
        {
            var recorder = new Mock<IRecorder>();
            var manualProgress = new Mock<IManualProgress>();
            var practiceTimeProgress = new Mock<IPracticeTimeProgress>();

            var speedProgress = new SpeedProgress(50, 100, 150, 100);

            using (var recordingExercise = new RecordingExercise(recorder.Object, "Exercise Title", speedProgress, practiceTimeProgress.Object, manualProgress.Object))
            {
                recordingExercise.DecrementSpeed(25);

                Assert.AreEqual(75, recordingExercise.CurrentSpeed);
                Assert.That(recordingExercise.CurrentSpeedProgress, Is.EqualTo(25));
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
