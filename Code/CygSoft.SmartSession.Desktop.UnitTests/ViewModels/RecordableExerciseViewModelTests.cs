﻿using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class RecordableExerciseViewModelTests
    {
        [Test]
        public void RecordableExerciseViewModel_Initial_State_Is_Correct()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
            Assert.AreEqual(false, viewModel.Recording);
            Assert.AreEqual(0, viewModel.Seconds);
            Assert.AreEqual("", viewModel.Status);
            Assert.AreEqual("Exercise Title", viewModel.Title);
        }

        [Test]
        public void RecordableExerciseViewModel_Initialized_RecordingStatus_Is_Not_Recording()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            Assert.That(viewModel.Status, Is.EqualTo(""));
        }

        [Test]
        public void RecordableExerciseViewModel_Recording_Reflects_Model_Recording()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

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
        public void RecordableExerciseViewModel_RecordingStatus_Gets_Value_From_ExerciseRecorder()
        {
            var exerciseRecorder = new ExerciseRecorder(new Recorder(), 1, "Exercise Title",
                new SpeedProgress(85, 85, 120, 10),
                new PracticeTimeProgress(300, 600, 10),
                new ManualProgress(0, 100));

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            exerciseRecorder.Resume();

            Assert.That(viewModel.Status, Is.EqualTo("RECORDING..."));
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(99, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_SpeedProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SpeedProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.IsTrue(changed);
        }






        [Test]
        public void RecordableExerciseViewModel_IncrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecordableExerciseViewModel_IncrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        public void RecordableExerciseViewModel_DecrementTime_Small_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementTime_Large_Raises_PropertyChanged_For_OverallProgress()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "OverallProgress")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecordableExerciseViewModel_IncrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecordableExerciseViewModel_IncrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementTime_Small_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }


        [Test]
        public void RecordableExerciseViewModel_DecrementTime_Large_Raises_PropertyChanged_For_RecordingTimeDisplay()
        {
            var changed = false;

            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "RecordingTimeDisplay")
                    changed = true;
            };

            viewModel.DecrementMinutesPracticedCommand.Execute(null);

            Assert.IsTrue(changed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.SmallSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(101, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(90, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Large_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(5, 7, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.LargeSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void RecordableExerciseViewModel_DecrementSpeed_Small_Below_Zero_Is_Zero()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(0, 0, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.SmallSpeedDecrementCommand.Execute(null);

            Assert.AreEqual(0, exerciseRecorder.CurrentSpeed);
            Assert.AreEqual(TimeFuncs.ZeroTimeDisplay, viewModel.RecordingTimeDisplay);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementSpeed_Large_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(60, 100, 160);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.LargeSpeedIncrementCommand.Execute(null);

            Assert.AreEqual(110, exerciseRecorder.CurrentSpeed);
        }

        [Test]
        public void RecordableExerciseViewModel_IncrementMinutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 600);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(60, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(360, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_Initialized_With_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            Assert.AreEqual(600, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(900, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_Decrement_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(10);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.DecrementMinutesPracticedCommand.Execute(null);
            
            Assert.AreEqual(540, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(840, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_Increment_RecordedTime_By_Minutes_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            exerciseRecorder.AddMinutes(1);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.IncrementMinutesPracticedCommand.Execute(null);

            Assert.AreEqual(120, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(420, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_Decrement_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(10, 1200);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.DecrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(9, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(19, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_Increment_RecordedTime_By_Seconds_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(10, 1200);
            exerciseRecorder.AddSeconds(10); // 20

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.IncrementSecondsPracticedCommand.Execute(null);

            Assert.AreEqual(11, exerciseRecorder.RecordedSeconds);
            Assert.AreEqual(21, exerciseRecorder.CurrentTotalSeconds);
        }

        [Test]
        public void RecordableExerciseViewModel_ManualProgress_Increment_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.IncrementManualProgressCommand.Execute(null);

            Assert.AreEqual(51, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void RecordableExerciseViewModel_ManualProgress_Decrement_Small_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.DecrementManualProgressCommand.Execute(null);

            Assert.AreEqual(49, exerciseRecorder.CurrentManualProgress);
        }

        [Test]
        public void RecordableExerciseViewModel_ManualProgress_Set_Reflects_On_Model()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(40);
            exerciseRecorder.IncrementManualProgress(10);

            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);
            viewModel.ManualProgress = 10;

            Assert.AreEqual(10, exerciseRecorder.CurrentManualProgress);
        }


        [Test]
        public void RecordableExerciseViewModel_OverallPracticeTime_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateTimeExerciseRecorder(300, 1200);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            exerciseRecorder.AddSeconds(300);

            Assert.AreEqual(TimeFuncs.DisplayTimeFromSeconds(600), viewModel.TotalRecordedDisplayTime);
        }

        [Test]
        public void RecordableExerciseViewModel_SpeedProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(30, 30, 90);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.SpeedProgress);
        }

        [Test]
        public void RecordableExerciseViewModel_OverallProgress_Reflects_Model_Dynamically()
        {
            var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(30, 30, 90);
            var viewModel = new RecordableExerciseViewModel(exerciseRecorder);

            exerciseRecorder.IncrementSpeed(30);

            Assert.AreEqual(50, viewModel.OverallProgress);
        }
    }
}
