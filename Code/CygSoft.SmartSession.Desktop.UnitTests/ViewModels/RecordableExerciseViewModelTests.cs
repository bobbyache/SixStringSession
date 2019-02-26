using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
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

            Assert.AreEqual("00:00:00", viewModel.DisplayTime);
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
    }
}
