using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class SingleExerciseRecorderViewModelTests
    {
        [Test]
        public void SingleExerciseRecorderViewModel_InitializeRecorder_Enables_CanExecute_StartRecording()
        {
            using (var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(10, 30, 50))
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();

                exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

                var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
                viewModel.InitializeRecorder(exerciseRecorder);

                Assert.That(viewModel.StartRecordingCommand.CanExecute(null), Is.True);
            }
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Pause_Correctly_Sets_Recording_State()
        {
            using (var exerciseRecorder = EntityFactory.CreateSpeedExerciseRecorder(50, 80, 100))
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();
                var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);

                viewModel.InitializeRecorder(exerciseRecorder);

                viewModel.StartRecordingCommand.Execute(null);
                var before = exerciseRecorder.Recording;
                viewModel.PauseRecordingCommand.Execute(null);
                var after = exerciseRecorder.Recording;

                Assert.IsTrue(before);
                Assert.IsFalse(after);
            }
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Start_Starts_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);

            viewModel.InitializeRecorder(exerciseRecorder.Object);

            // Pretend that I'm paused.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(false);
            // Now start!
            viewModel.StartRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Resume(), Times.Once, "Start() should have been invoked.");
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Pause_Pauses_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder.Object);

            // Pretend that I'm recording.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(true);
            // Now pause!
            viewModel.PauseRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Pause(), Times.Once, "Pause() should have been invoked.");
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Start_WhenStarted_DoesNot_Invoke_Start_On_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);

            viewModel.InitializeRecorder(exerciseRecorder.Object);

            // Pretend that already recording.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(true);

            // try starting...
            viewModel.StartRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Resume(), Times.Never,
                "Start() should not have been called because the recorder is is already recording.");
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Pause_WhenPaused_DoesNot_Invoke_Pause_On_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);

            viewModel.InitializeRecorder(exerciseRecorder.Object);

            // Pretend that already paused.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(false);

            // try pausing...
            viewModel.PauseRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Pause(), Times.Never,
                "Pause() should not have been called because the recorder is is already paused.");
        }

        //[Test]
        //public void SingleExerciseRecorderViewModel_Play_Pause_Cancel_Then_Initialize_StartButton_Enabled()
        //{
        //    using (var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(0))
        //    {
        //        var exerciseService = new Mock<IExerciseService>();
        //        var dialogService = new Mock<IDialogViewService>();

        //        var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);

        //        viewModel.InitializeRecorder(exerciseRecorder);
        //        viewModel.StartRecordingCommand.Execute(null);
        //        viewModel.PauseRecordingCommand.Execute(null);

        //        // Now cancel out...
        //        viewModel.CancelRecordingCommand.Execute(null);

        //        var recordingChangedEventFired = false;

        //        viewModel.RecordingStatusChanged += (s, e) => recordingChangedEventFired = true;

        //        // Come in again and ensure that exercise.Recording is false.
        //        viewModel.InitializeRecorder(exerciseRecorder);

        //        Assert.That(recordingChangedEventFired, Is.True,
        //            "RecordingStatusChanged did not fire as it was supposed to.");
        //    }
        // }

        [Test]
        public void SingleExerciseRecorderViewModel_SaveRecording_Invokes_ExerciseRecorder_SaveRecording()
        {
            var exerciseRecorder = new Mock<IExerciseRecorder>();
            exerciseRecorder.Setup(recorder => recorder.RecordedSeconds).Returns(300);  // required for positive validation.
            exerciseRecorder.Setup(recorder => recorder.Recording).Returns(false);      // required for positive validation.

            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder.Object);
            viewModel.MetronomeSpeed = 50;                                              // required for positive validation.

            viewModel.SaveRecordingCommand.Execute(null);

            exerciseRecorder.Verify(recorder => recorder.SaveRecording(exerciseService.Object), Times.Once());
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Change_Speed_AssignsTo_Recorder()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(0);
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder);
            viewModel.MetronomeSpeed = 50; 

            Assert.That(exerciseRecorder.CurrentSpeed, Is.EqualTo(viewModel.MetronomeSpeed));
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Change_Speed_Raises_PropertyChanged()
        {
            var changed = false;
            var propertyName = "";

            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(0);
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder);
            viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                propertyName = e.PropertyName;
            };

            viewModel.MetronomeSpeed = 50;

            Assert.AreEqual("MetronomeSpeed", propertyName);
            Assert.IsTrue(changed);
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Change_ManualProgress_AssignsTo_Recorder()
        {
            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(0);
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder);
            viewModel.ManualProgress = 50;

            Assert.That(exerciseRecorder.CurrentManualProgress, Is.EqualTo(viewModel.ManualProgress));
        }

        [Test]
        public void SingleExerciseRecorderViewModel_Change_ManualProgress_Raises_PropertyChanged()
        {
            var changed = false;
            var propertyName = "";

            var exerciseRecorder = EntityFactory.CreateManualExerciseRecorder(0);
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var viewModel = new SingleExerciseRecorderViewModel(exerciseService.Object, dialogService.Object);
            viewModel.InitializeRecorder(exerciseRecorder);
            viewModel.PropertyChanged += (s, e) =>
            {
                changed = true;
                if (e.PropertyName == "ManualProgress")
                    propertyName = e.PropertyName;
            };

            viewModel.ManualProgress = 50;

            Assert.AreEqual("ManualProgress", propertyName);
            Assert.IsTrue(changed);
        }

        private Exercise GetTestExercise(int id)
        {
            var exercise = new Exercise()
            {
                Title = "Test Exercise",
                Id = id,
                DateCreated = new DateTime(2018, 9, 12),
                DateModified = new DateTime(2018, 9, 12),
                TargetMetronomeSpeed = 120,
                TargetPracticeTime = 3000
            };

            return exercise;
        }
    }
}
