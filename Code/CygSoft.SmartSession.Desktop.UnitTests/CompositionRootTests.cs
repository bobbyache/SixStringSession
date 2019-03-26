using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Domain.Recording;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class CompositionRootTests
    {
        [Test]
        public void CompositeRoot_ViewModelFactory_Creates_RecorderViewModel_Successfully()
        {
            var exerciseRecorder = new Mock<IExerciseRecorder>();
            ViewModelLocator compositeRoot = new ViewModelLocator();

            var factory = compositeRoot.ViewModelFactory;

            var viewModel = factory.CreateRecorderViewModel(exerciseRecorder.Object);
            factory.Release(viewModel);

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_ViewModelFactory_Creates_RecorderViewModel_AsSingleton()
        {
            ViewModelLocator compositeRoot = new ViewModelLocator();
            var factory = compositeRoot.ViewModelFactory;

            var viewModel_A = factory.CreateRecorderViewModel(new Mock<IExerciseRecorder>().Object);
            var viewModel_B = factory.CreateRecorderViewModel(new Mock<IExerciseRecorder>().Object);

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        //var recorder = compositeRoot.ComponentFactory.CreateRecorder();
        //compositeRoot.ComponentFactory.Release(recorder);

        //var manualProgress = compositeRoot.ComponentFactory.CreateManualProgress(600, 300);
        //compositeRoot.ComponentFactory.Release(manualProgress);

        //var practiceTimeProgress = compositeRoot.ComponentFactory.CreatePracticeTimeProgress(200, 1200, 300);
        //compositeRoot.ComponentFactory.Release(practiceTimeProgress);

        //var speedProgress = compositeRoot.ComponentFactory.CreateSpeedProgress(20, 80, 160, 300);
        //compositeRoot.ComponentFactory.Release(speedProgress);

        //var exerciseRecorder = new ExerciseRecorder(recorder, 3, "My Title", speedProgress, practiceTimeProgress, manualProgress);
        //Assert.That(exerciseRecorder, Is.Not.Null);

        //ViewModelLocator compositeRoot = new ViewModelLocator();
        //var factory = compositeRoot.ViewModelFactory;
        //var obj = factory.CreateRecorderViewModel(exerciseRecorder);
    }
}
