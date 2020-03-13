using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Desktop.Supports.Factories;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using CygSoft.SmartSession.Infrastructure.Enums;
using Moq;
using NUnit.Framework;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class ExerciseCompositeViewModelTests
    {
        [Test]
        public void ExerciseCompositeViewModel_Receives_Save_Message_From_ExerciseEditViewModel()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var exercise = new Exercise { Id = 0, Title = "New Exercise" };
            exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                .Returns(exercise);

            var exerciseManagementViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);
            var viewModelFactory = new Mock<IViewModelFactory>();

            var viewModel = new MockExerciseCompositeViewModel(viewModelFactory.Object, exerciseService.Object, exerciseManagementViewModel, exerciseEditViewModel);

            exerciseEditViewModel.StartEdit(exercise);
            exerciseEditViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.EndEditinExerciseCalled);

            Assert.That(viewModel.EndEditOperation, Is.EqualTo(EditorCloseOperation.Saved));
        }

        [Test]
        public void ExerciseCompositeViewModel_Receives_Cancel_Message_From_ExerciseEditViewModel()
        {
            ViewModelLocator compositeRoot = new ViewModelLocator();
            var viewModelFactory = compositeRoot.ViewModelFactory;

            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var exercise = new Exercise { Id = 2, Title = "Existing Exercise" };
            exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                .Returns(exercise);

            var exerciseManagementViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);

            var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);

            var viewModel = new MockExerciseCompositeViewModel(viewModelFactory, exerciseService.Object, exerciseManagementViewModel, exerciseEditViewModel);

            exerciseEditViewModel.StartEdit(exercise);
            exerciseEditViewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.EndEditinExerciseCalled);
            Assert.That(viewModel.EndEditOperation, Is.EqualTo(EditorCloseOperation.Canceled));
        }


        [Test]
        public void ExerciseCompositeViewModel_Calls_Save_On_Service_When_Exercise_Saved_AsExisting()
        {
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var exercise = new Exercise { Id = 2, Title = "Existing Exercise" };
            exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                .Returns(exercise);

            var exerciseSearchViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);
            var viewModelFactory = new Mock<IViewModelFactory>();

            var viewModel = new ExerciseCompositeViewModel(viewModelFactory.Object, exerciseService.Object, exerciseSearchViewModel, exerciseEditViewModel);

            exerciseEditViewModel.StartEdit(exercise);
            exerciseEditViewModel.SaveCommand.Execute(null);

            exerciseService.Verify(svc => svc.Update(It.IsAny<Exercise>()),
                Times.Once, "IExerciseService.Update was not called.");
        }

        [Test]
        public void ExerciseCompositeViewModel_Calls_Add_On_Service_When_Exercise_Saved_AsNew()
        {
            var dialogService = new Mock<IDialogViewService>();

            var exerciseService = new Mock<IExerciseService>();
            var exercise = new Exercise { Id = 0, Title = "New Exercise" };
            exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                .Returns(exercise);

            var exerciseManagementViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);
            var viewModelFactory = new Mock<IViewModelFactory>();


            var viewModel = new ExerciseCompositeViewModel(viewModelFactory.Object, exerciseService.Object, exerciseManagementViewModel, exerciseEditViewModel);

            exerciseEditViewModel.StartEdit(exercise);
            exerciseEditViewModel.SaveCommand.Execute(null);

            exerciseService.Verify(svc => svc.Add(It.IsAny<Exercise>()),
                Times.Once, "IExerciseService.Add was not called.");
        }
    }

    public class MockExerciseCompositeViewModel : ExerciseCompositeViewModel
    {
        public bool EndEditinExerciseCalled = false;
        public EditorCloseOperation? EndEditOperation;
        public EntityLifeCycleState EndEditLifeCycleState;

        public MockExerciseCompositeViewModel(IViewModelFactory viewModelFactory, IExerciseService exerciseService, ExerciseManagementViewModel exerciseSearchViewModel,
            ExerciseEditViewModel exerciseEditViewModel) : base(viewModelFactory, exerciseService, exerciseSearchViewModel, exerciseEditViewModel)
        {

        }

        protected override void EndEditingExercise(IExercise exercise, EditorCloseOperation operation, EntityLifeCycleState entityLifeCycleState)
        {
            EndEditinExerciseCalled = true;
            EndEditOperation = operation;
            EndEditLifeCycleState = entityLifeCycleState;
        }
    }
}
