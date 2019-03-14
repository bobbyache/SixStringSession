using CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class RecorderViewModelTests
    {
        [Test]
        public void RecorderViewModel_Initial_State_Is_Correct()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecorderViewModel(exerciseRecorder);

            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
            Assert.AreEqual(false, viewModel.Recording);
            Assert.AreEqual(0, viewModel.Seconds);
            Assert.AreEqual("", viewModel.Status);
            Assert.AreEqual("Exercise Title", viewModel.Title);
        }

        [Test]
        public void RecorderViewModel_Initialized_RecordingStatus_Is_Not_Recording()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecorderViewModel(exerciseRecorder);

            Assert.That(viewModel.Status, Is.EqualTo(""));
        }

        [Test]
        public void RecorderViewModel_Recording_Reflects_Model_Recording()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecorderViewModel(exerciseRecorder);

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
        public void RecorderViewModel_RecordingStatus_Gets_Value_From_ExerciseRecorder()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecorderViewModel(exerciseRecorder);

            exerciseRecorder.Resume();

            Assert.That(viewModel.Status, Is.EqualTo("RECORDING..."));
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(99, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }






        [Test]
        public void RecorderViewModel_IncrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecorderViewModel_IncrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        public void RecorderViewModel_DecrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_DecrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecorderViewModel_IncrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecorderViewModel_IncrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_DecrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecorderViewModel_DecrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(101, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(90, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Large_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(5, 7, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void RecorderViewModel_DecrementSpeed_Small_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(0, 0, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void RecorderViewModel_IncrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(110, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecorderViewModel_IncrementMinutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 600);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(60, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(360, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_Initialized_With_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new RecorderViewModel(exerciseRecorder);

            Assert.AreEqual(600, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(900, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_Decrement_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.DecrementMinutesPracticedCommand.Execute(null);
            
            Assert.AreEqual(540, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(840, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_Increment_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(1);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(120, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(420, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_Decrement_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(10, 1200);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(9, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(19, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_Increment_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(10, 1200);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(11, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(21, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecorderViewModel_ManualProgress_Increment_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.IncrementManualProgressCommand.Execute(null);

            Assert.AreEqual(51, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void RecorderViewModel_ManualProgress_Decrement_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.DecrementManualProgressCommand.Execute(null);

            Assert.AreEqual(49, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void RecorderViewModel_ManualProgress_Set_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecorderViewModel(exerciseRecorder);
            viewModel.ManualProgress = 10;

            Assert.AreEqual(10, exerciseRecorder.CurrentManualProgress);
        }


        [Test]
        public void RecorderViewModel_OverallPracticeTime_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            exerciseRecorder.AddSeconds(300);

            Assert.AreEqual(TimeFuncs.DisplayTimeFromSeconds(600), viewModel.TotalRecordedDisplayTime);
        }

        [Test]
        public void RecorderViewModel_SpeedProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(30, 30, 90);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.SpeedProgress);
        }

        [Test]
        public void RecorderViewModel_OverallProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(30, 30, 90);
            var viewModel = new RecorderViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.OverallProgress);
        }
    }
}
