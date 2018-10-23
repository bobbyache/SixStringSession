using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests.Recorder
{
    [TestFixture]
    public class ExerciseRecorderViewModelTests
    {
        [Test]
        public void ExerciseRecorderViewModel_InitializeRecorder_Enables_CanExecute_StartRecording()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object, exerciseRecorder.Object);
            viewModel.InitializeRecorder(3);

            Assert.That(viewModel.StartRecordingCommand.CanExecute(null), Is.True);
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_Correctly_Sets_Recording_State()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new ExerciseRecorder();

            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object, 
                exerciseRecorder);

            viewModel.InitializeRecorder(3);

            viewModel.StartRecordingCommand.Execute(null);
            var before = exerciseRecorder.Recording;
            viewModel.PauseRecordingCommand.Execute(null);
            var after = exerciseRecorder.Recording;

            Assert.IsTrue(before);
            Assert.IsFalse(after);
        }

        [Test]
        public void ExerciseRecorderViewModel_Start_Starts_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object,
                exerciseRecorder.Object);

            viewModel.InitializeRecorder(3);

            // Pretend that I'm paused.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(false);
            // Now start!
            viewModel.StartRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Start(), Times.Once, "Start() should have been invoked.");
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_Pauses_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();
            
            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object,
                exerciseRecorder.Object);
            viewModel.InitializeRecorder(3);

            // Pretend that I'm recording.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(true);
            // Now pause!
            viewModel.PauseRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Pause(), Times.Once, "Pause() should have been invoked.");
        }

        [Test]
        public void ExerciseRecorderViewModel_Start_WhenStarted_DoesNot_Invoke_Start_On_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object,
                exerciseRecorder.Object);

            viewModel.InitializeRecorder(3);

            // Pretend that already recording.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(true);

            // try starting...
            viewModel.StartRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Start(), Times.Never, 
                "Start() should not have been called because the recorder is is already recording.");
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_WhenPaused_DoesNot_Invoke_Pause_On_ExerciseRecorder()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();
            var exerciseRecorder = new Mock<IExerciseRecorder>();

            exerciseService.Setup(svc => svc.Get(It.IsAny<int>())).Returns(GetTestExercise(1));

            ExerciseRecorderViewModel viewModel = new ExerciseRecorderViewModel(exerciseService.Object, dialogService.Object,
                exerciseRecorder.Object);

            viewModel.InitializeRecorder(3);

            // Pretend that already paused.
            exerciseRecorder.Setup(rec => rec.Recording).Returns(false);

            // try pausing...
            viewModel.PauseRecordingCommand.Execute(null);

            exerciseRecorder.Verify(m => m.Pause(), Times.Never,
                "Pause() should not have been called because the recorder is is already paused.");
        }

        private Exercise GetTestExercise(int id)
        {
            var exercise = new Exercise()
            {
                Title = "Test Exercise",
                Id = id,
                DateCreated = new DateTime(2018, 9, 12),
                DateModified = new DateTime(2018, 9, 12),
                DifficultyRating = 3,
                PracticalityRating = 3,
                TargetMetronomeSpeed = 120,
                TargetPracticeTime = 3000,
                PercentageCompleteCalculationType = Infrastructure.Enums.PercentCompleteCalculationStrategy.MetronomeSpeed
            };

            return exercise;
        }
    }
}
