using CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class TimeSlotRecorderViewModelTests
    {
        [Test]
        public void TimeSlotRecorderViewModel_AddSeconds_Raises_PropertyChanged_For_RemainingTimeDisplay()
        {
            var changed = false;

            int recordedSeconds = 300;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RemainingTimeDisplay")
                    changed = true;
            };

            exerciseRecorder.AddSeconds(200);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_AddSeconds_Raises_PropertyChanged_For_AssignedTimeDisplay()
        {
            var changed = false;

            int recordedSeconds = 300;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AssignedTimeDisplay")
                    changed = true;
            };

            exerciseRecorder.AddSeconds(200);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_AddSeconds_Raises_PropertyChanged_For_AssignedSeconds()
        {
            var changed = false;

            int recordedSeconds = 300;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AssignedSeconds")
                    changed = true;
            };

            exerciseRecorder.AddSeconds(200);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_AddSeconds_Raises_PropertyChanged_For_RemainingSeconds()
        {
            var changed = false;

            int recordedSeconds = 300;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RemainingSeconds")
                    changed = true;
            };

            exerciseRecorder.AddSeconds(200);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_AddSeconds_Shows_Initial_and_Remaining()
        {
            int recordedSeconds = 0;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.AddSeconds(480);

            Assert.AreEqual(600, viewModel.AssignedSeconds);
            Assert.AreEqual(120, viewModel.RemainingSeconds);
            Assert.AreEqual("00:10:00", viewModel.AssignedTimeDisplay);
            Assert.AreEqual("00:02:00", viewModel.RemainingTimeDisplay);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Initial_State_Has_Initial_and_Remaining()
        {
            int recordedSeconds = 300;
            int assignedTime = 600;

            var exerciseRecorder = new TimeSlotExerciseRecorder(new TestRecorder(recordedSeconds), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), assignedTime);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            Assert.AreEqual(600, viewModel.AssignedSeconds);
            Assert.AreEqual(300, viewModel.RemainingSeconds);
            Assert.AreEqual("00:10:00", viewModel.AssignedTimeDisplay);
            Assert.AreEqual("00:05:00", viewModel.RemainingTimeDisplay);
        }

        public class TestRecorder : Recorder
        {
            public TestRecorder(double initialSeconds) : base()
            {
                base.recordedSeconds = initialSeconds;
            }
        }

        [Test]
        public void TimeSlotRecorderViewModel_Initial_State_Is_Correct()
        {
            var exerciseRecorder = new TimeSlotExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
            Assert.AreEqual(false, viewModel.Recording);
            Assert.AreEqual(0, viewModel.Seconds);
            Assert.AreEqual("", viewModel.Status);
            Assert.AreEqual("Exercise Title", viewModel.Title);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Initialized_RecordingStatus_Is_Not_Recording()
        {
            var exerciseRecorder = new TimeSlotExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            Assert.That(viewModel.Status, Is.EqualTo(""));
        }

        [Test]
        public void TimeSlotRecorderViewModel_Recording_Reflects_Model_Recording()
        {
            var exerciseRecorder = new TimeSlotExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.Resume();
            var recording = exerciseRecorder.Recording && viewModel.Recording;

            exerciseRecorder.Pause();
            var paused = !exerciseRecorder.Recording && !viewModel.Recording;

            exerciseRecorder.Resume();
            exerciseRecorder.Reset();
            var reset = !exerciseRecorder.Recording && !viewModel.Recording;

            Assert.IsTrue(recording);
            Assert.IsTrue(paused);
            Assert.IsTrue(reset);
        }

        [Test]
        public void TimeSlotRecorderViewModel_RecordingStatus_Gets_Value_From_TimeSlotExerciseRecorder()
        {
            var exerciseRecorder = new TimeSlotExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100), 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.Resume();

            Assert.That(viewModel.Status, Is.EqualTo("RECORDING..."));
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(99, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void TimeSlotRecorderViewModel_IncrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        public void TimeSlotRecorderViewModel_DecrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void TimeSlotRecorderViewModel_IncrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void TimeSlotRecorderViewModel_IncrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void TimeSlotRecorderViewModel_DecrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(101, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(90, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Large_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(5, 7, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void TimeSlotRecorderViewModel_DecrementSpeed_Small_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(0, 0, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(60, 100, 160, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(110, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void TimeSlotRecorderViewModel_IncrementMinutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(300, 600, 600);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(60, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(360, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Initialized_With_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(300, 1200, 600);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            Assert.AreEqual(600, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(900, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Decrement_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(300, 1200, 600);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(540, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(840, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Increment_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(300, 1200, 600);
            exerciseRecorder.AddMinutes(1);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(120, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(420, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Decrement_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(10, 1200, 600);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(9, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(19, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_Increment_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(10, 1200, 600);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(11, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(21, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void TimeSlotRecorderViewModel_ManualProgress_Increment_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualProgressTimeSlotExerciseRecorder(40, 600);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.IncrementManualProgressCommand.Execute(null);

            Assert.AreEqual(51, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void TimeSlotRecorderViewModel_ManualProgress_Decrement_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualProgressTimeSlotExerciseRecorder(40, 600);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.DecrementManualProgressCommand.Execute(null);

            Assert.AreEqual(49, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void TimeSlotRecorderViewModel_ManualProgress_Set_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualProgressTimeSlotExerciseRecorder(40, 600);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);
            viewModel.ManualProgress = 10;

            Assert.AreEqual(10, exerciseRecorder.CurrentManualProgress);
        }


        [Test]
        public void TimeSlotRecorderViewModel_OverallPracticeTime_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateTimeProgressTimeSlotExerciseRecorder(300, 1200, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.AddSeconds(300);

            Assert.AreEqual(TimeFuncs.DisplayTimeFromSeconds(600), viewModel.TotalRecordedDisplayTime);
        }

        [Test]
        public void TimeSlotRecorderViewModel_SpeedProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(30, 30, 90, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.SpeedProgress);
        }

        [Test]
        public void TimeSlotRecorderViewModel_OverallProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedProgressTimeSlotExerciseRecorder(30, 30, 90, 600);
            var viewModel = new TimeSlotRecorderViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.OverallProgress);
        }
    }
}
