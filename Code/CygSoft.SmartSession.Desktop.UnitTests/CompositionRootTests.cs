using CygSoft.SmartSession.Desktop.Supports;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Domain.Recording;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class CompositionRootTests
    {

        [SetUp]
        public void SetupBeforeTest()
        {
            var dbConnection = new Mock<IDbConnection>();
            var settings = new Mock<ISettings>();
            settings.Setup(s => s.ConnectionString).Returns("");
            settings.Setup(s => s.DatabaseConnection).Returns(dbConnection.Object);
            Settings.AppSettings = settings.Object;
        }

        [TearDown]
        public void TearDownAfterTest()
        {
            Settings.AppSettings = new Settings();
        }

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
        public void CompositeRoot_ViewModelFactory_Creates_New_RecorderViewModel_EveryTime()
        {
            ViewModelLocator compositeRoot = new ViewModelLocator();
            var factory = compositeRoot.ViewModelFactory;

            var viewModel_A = factory.CreateRecorderViewModel(new Mock<IExerciseRecorder>().Object);
            var viewModel_B = factory.CreateRecorderViewModel(new Mock<IExerciseRecorder>().Object);

            Assert.That(viewModel_A, Is.Not.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseEditModel_Successfully()
        {
            ViewModelLocator compositeRoot = new ViewModelLocator();

            var viewModel = compositeRoot.ExerciseEditViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseEditModel_OnlyOnce()
        {
            ViewModelLocator compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.ExerciseEditViewModel;
            var viewModel_B = compositeRoot.ExerciseEditViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseEditViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.ExerciseEditViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseEditViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.ExerciseEditViewModel;
            var viewModel_B = compositeRoot.ExerciseEditViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseSearchViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.ExerciseSearchViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseSearchViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.ExerciseSearchViewModel;
            var viewModel_B = compositeRoot.ExerciseSearchViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseSelectionViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.ExerciseSelectionViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_ExerciseSelectionViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.ExerciseSelectionViewModel;
            var viewModel_B = compositeRoot.ExerciseSelectionViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_PracticeRoutineEditViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.PracticeRoutineEditViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_PracticeRoutineEditViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.PracticeRoutineEditViewModel;
            var viewModel_B = compositeRoot.PracticeRoutineEditViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_PracticeRoutineManagementViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.PracticeRoutineManagementViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_PracticeRoutineManagementViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.PracticeRoutineManagementViewModel;
            var viewModel_B = compositeRoot.PracticeRoutineManagementViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }

        [Test]
        public void CompositeRoot_Creates_A_RoutineRecorderViewModel_Successfully()
        {
            var compositeRoot = new ViewModelLocator();
            var viewModel = compositeRoot.RoutineRecorderViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void CompositeRoot_Creates_A_RoutineRecorderViewModel_OnlyOnce()
        {
            var compositeRoot = new ViewModelLocator();

            var viewModel_A = compositeRoot.RoutineRecorderViewModel;
            var viewModel_B = compositeRoot.RoutineRecorderViewModel;

            Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        }


        // ---------------------------------------------------------------------------------------

        //TODO: Enabling these tests creates a problem here:
        // ExerciseCompositeViewModel_Calls_Add_On_Service_When_Exercise_Saved_AsNew
        // ExerciseCompositeViewModel_Calls_Save_On_Service_When_Exercise_Saved_AsExisting
        // ExerciseCompositeViewModel_Receives_Save_Message_From_ExerciseEditViewModel
        // but all of these tests use the CompositeRoot. They shouldn't!


        //[Test]
        //public void CompositeRoot_Creates_A_ExerciseCompositeViewModel_Successfully()
        //{
        //    var compositeRoot = new ViewModelLocator();
        //    var viewModel = compositeRoot.ExerciseCompositeViewModel;

        //    Assert.That(viewModel, Is.Not.Null);
        //}

        //[Test]
        //public void CompositeRoot_Creates_A_ExerciseCompositeViewModel_OnlyOnce()
        //{
        //    var compositeRoot = new ViewModelLocator();

        //    var viewModel_A = compositeRoot.ExerciseCompositeViewModel;
        //    var viewModel_B = compositeRoot.ExerciseCompositeViewModel;

        //    Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        //}

        // ---------------------------------------------------------------------------------------

        //TODO: How can one run these tests without touching the database?
        //[Test]
        //public void CompositeRoot_Creates_A_PracticeRoutineCompositeViewModel_Successfully()
        //{
        //    var compositeRoot = new ViewModelLocator();
        //    var viewModel = compositeRoot.PracticeRoutineCompositeViewModel;

        //    Assert.That(viewModel, Is.Not.Null);
        //}

        //[Test]
        //public void CompositeRoot_Creates_A_PracticeRoutineCompositeViewModel_OnlyOnce()
        //{
        //    var compositeRoot = new ViewModelLocator();

        //    var viewModel_A = compositeRoot.PracticeRoutineCompositeViewModel;
        //    var viewModel_B = compositeRoot.PracticeRoutineCompositeViewModel;

        //    Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        //}

        //[Test]
        //public void CompositeRoot_Creates_A_MainWindowViewModel_Successfully()
        //{
        //    var compositeRoot = new ViewModelLocator();
        //    var viewModel = compositeRoot.MainWindowViewModel;

        //    Assert.That(viewModel, Is.Not.Null);
        //}

        //[Test]
        //public void CompositeRoot_Creates_A_MainWindowViewModel_OnlyOnce()
        //{
        //    var compositeRoot = new ViewModelLocator();

        //    var viewModel_A = compositeRoot.MainWindowViewModel;
        //    var viewModel_B = compositeRoot.MainWindowViewModel;

        //    Assert.That(viewModel_A, Is.SameAs(viewModel_B));
        //}

    }
}
