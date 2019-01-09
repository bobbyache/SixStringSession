using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Domain.Exercises;
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
            var exercise = new Mock<IExercise>();
            exercise.Setup(ex => ex.Title).Returns("Exercise Title");
            exercise.Setup(ex => ex.GetSecondsPracticed()).Returns(300); // 5 min
            exercise.Setup(ex => ex.GetPercentComplete()).Returns(50);
            exercise.Setup(ex => ex.GetLastRecordedSpeed()).Returns(85);
            exercise.Setup(ex => ex.GetLastRecordedManualProgress()).Returns(40);
            exercise.Setup(ex => ex.ManualProgressWeighting).Returns(100);
            exercise.Setup(ex => ex.PracticeTimeProgressWeighting).Returns(10);
            exercise.Setup(ex => ex.SpeedProgressWeighting).Returns(10);
            exercise.Setup(ex => ex.TargetMetronomeSpeed).Returns(120);
            exercise.Setup(ex => ex.TargetPracticeTime).Returns(600); // 10 min

            var viewModel = new RecordableExerciseViewModel(exercise.Object);

            Assert.AreEqual("00:00:00", viewModel.DisplayTime);
            Assert.AreEqual(false, viewModel.Recording);
            Assert.AreEqual(0, viewModel.Seconds);
            Assert.AreEqual("", viewModel.Status);
            Assert.AreEqual("Exercise Title", viewModel.Title);
            Assert.IsNotNull(viewModel.Exercise);
        }
    }
}
