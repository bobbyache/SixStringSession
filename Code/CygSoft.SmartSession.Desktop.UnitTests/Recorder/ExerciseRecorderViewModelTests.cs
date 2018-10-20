using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests.Recorder
{
    [TestFixture]
    public class ExerciseRecorderViewModelTests
    {
        //private class TestExerciseRecorderViewModel : ExerciseRecorderViewModel
        //{

        //    //public TestExerciseRecorderViewModel() : base(
        //    //    new Mock<IExerciseService>().Object, new Mock<IDialogViewService>().Object)
        //    //{

        //    //}
        //    //public TestExerciseRecorderViewModel(ExerciseRecorder exerciseRecorder) : base(exerciseRecorder)
        //    //{

        //    //}
        //    //public bool Timing { get { return base.timing; } set { base.timing = value; } }
        //}

        [Test]
        public void ExerciseRecorderViewModel_Start_Correctly_Sets_Timing_State()
        {
            //TestExerciseRecorderViewModel viewModel = new TestExerciseRecorderViewModel();
            //var before = viewModel.Timing;

            //viewModel.StartRecordingCommand.Execute(null);

            //var after = viewModel.Timing;

            //Assert.IsFalse(before);
            //Assert.IsTrue(after);

            Assert.Fail();
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_Correctly_Sets_Timing_State()
        {
            //TestExerciseRecorderViewModel viewModel = new TestExerciseRecorderViewModel();

            //viewModel.StartRecordingCommand.Execute(null);
            //var before = viewModel.Timing;
            //viewModel.PauseRecordingCommand.Execute(null);
            //var after = viewModel.Timing;

            //Assert.IsTrue(before);
            //Assert.IsFalse(after);

            Assert.Fail();
        }

        [Test]
        public void ExerciseRecorderViewModel_Start_Starts_ExerciseRecorder()
        {
            //var dialogService = new Mock<IDialogViewService>();
            //var exerciseService = new Mock<IExerciseService>();
            //var recorderMock = new Mock<ExerciseRecorder>();

            //var viewModel = new TestExerciseRecorderViewModel(recorderMock.Object);

            //viewModel.StartRecordingCommand.Execute(null);

            //recorderMock.Verify(m => m.Start(), Times.Once, "Start() should have been invoked.");

            Assert.Fail();
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_Pauses_ExerciseRecorder()
        {
            //var recorderMock = new Mock<ExerciseRecorder>();
            //var viewModel = new TestExerciseRecorderViewModel(recorderMock.Object);

            //viewModel.StartRecordingCommand.Execute(null);
            //viewModel.PauseRecordingCommand.Execute(null);

            //recorderMock.Verify(m => m.Pause(), Times.Once, "Pause() should have been invoked.");
            Assert.Fail();
        }

        [Test]
        public void ExerciseRecorderViewModel_Start_WhenStarted_DoesNot_Invoke_Start_On_ExerciseRecorder()
        {
            //var recorderMock = new Mock<ExerciseRecorder>();
            //var viewModel = new TestExerciseRecorderViewModel(recorderMock.Object);

            //viewModel.StartRecordingCommand.Execute(null);
            //viewModel.StartRecordingCommand.Execute(null);

            //recorderMock.Verify(m => m.Start(), Times.Once, "Start() should only have been invoked once.");

            Assert.Fail();
        }

        [Test]
        public void ExerciseRecorderViewModel_Pause_WhenPaused_DoesNot_Invoke_Pause_On_ExerciseRecorder()
        {
            //var recorderMock = new Mock<ExerciseRecorder>();
            //var viewModel = new TestExerciseRecorderViewModel(recorderMock.Object);

            //viewModel.StartRecordingCommand.Execute(null);
            //viewModel.PauseRecordingCommand.Execute(null);
            //viewModel.PauseRecordingCommand.Execute(null);

            //recorderMock.Verify(m => m.Pause(), Times.Once, "Pause() should only have been invoked once.");

            Assert.Fail();
        }
    }
}
