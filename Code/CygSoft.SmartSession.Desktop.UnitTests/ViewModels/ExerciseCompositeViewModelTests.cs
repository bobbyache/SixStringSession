using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
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
            //var exerciseRecorder = new Mock<IExerciseRecorder>();
            //var exerciseService = new Mock<IExerciseService>();
            //var dialogService = new Mock<IDialogViewService>();

            //var exercise = new Exercise { Id = 0, Title = "New Exercise" };
            //exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
            //    .Returns(exercise);

            //var exerciseSearchViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            //var recorderViewModel = new RecorderViewModel(exerciseRecorder.Object);

            //var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);

            //var viewModel = new MockExerciseCompositeViewModel(exerciseService.Object, exerciseSearchViewModel, exerciseEditViewModel, recorderViewModel);

            //exerciseEditViewModel.StartEdit(exercise);
            //exerciseEditViewModel.SaveCommand.Execute(null);

            //Assert.IsTrue(viewModel.EndEditinExerciseCalled);

            //Assert.That(viewModel.EndEditOperation, Is.EqualTo(EditorCloseOperation.Saved));
            Assert.Fail("Need to put recorderViewModel back!!!!!!!!");
        }

        [Test]
        public void ExerciseCompositeViewModel_Receives_Cancel_Message_From_ExerciseEditViewModel()
        {
            //var exerciseRecorder = new Mock<IExerciseRecorder>();
            //var exerciseService = new Mock<IExerciseService>();
            //var dialogService = new Mock<IDialogViewService>();

            //var exercise = new Exercise { Id = 2, Title = "Existing Exercise" };
            //exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
            //    .Returns(exercise);

            //var exerciseSearchViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            //var recorderViewModel = new RecorderViewModel(exerciseRecorder.Object);

            //var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);

            //var viewModel = new MockExerciseCompositeViewModel(exerciseService.Object, exerciseSearchViewModel, exerciseEditViewModel, recorderViewModel);

            //exerciseEditViewModel.StartEdit(exercise);
            //exerciseEditViewModel.CancelCommand.Execute(null);

            //Assert.IsTrue(viewModel.EndEditinExerciseCalled);
            //Assert.That(viewModel.EndEditOperation, Is.EqualTo(EditorCloseOperation.Canceled));
            Assert.Fail("Need to put recorderViewModel back!!!!!!!!");
        }


        [Test]
        public void ExerciseCompositeViewModel_Calls_Save_On_Service_When_Exercise_Saved_AsExisting()
        {
            var exerciseRecorder = new Mock<IExerciseRecorder>();
            var exerciseService = new Mock<IExerciseService>();
            var dialogService = new Mock<IDialogViewService>();

            var exercise = new Exercise { Id = 2, Title = "Existing Exercise" };
            exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                .Returns(exercise);

            var exerciseSearchViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            var recorderViewModel = new RecorderViewModel(exerciseRecorder.Object);

            var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);

            var viewModel = new ExerciseCompositeViewModel(exerciseService.Object, exerciseSearchViewModel, exerciseEditViewModel
                //, 
                //recorderViewModel
                );
            Assert.Fail("Need to put recorderViewModel back!!!!!!!!");

            exerciseEditViewModel.StartEdit(exercise);
            exerciseEditViewModel.SaveCommand.Execute(null);

            exerciseService.Verify(svc => svc.Update(It.IsAny<Exercise>()),
                Times.Once, "IExerciseService.Update was not called.");

        }

        [Test]
        public void ExerciseCompositeViewModel_Calls_Add_On_Service_When_Exercise_Saved_AsNew()
        {
            //var exerciseRecorder = new Mock<IExerciseRecorder>();
            //var exerciseService = new Mock<IExerciseService>();
            //var dialogService = new Mock<IDialogViewService>();

            //var exercise = new Exercise { Id = 0, Title = "New Exercise" };
            //exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
            //    .Returns(exercise);

            //var exerciseSearchViewModel = new ExerciseManagementViewModel(exerciseService.Object, dialogService.Object);
            //var recorderViewModel = new RecorderViewModel(exerciseRecorder.Object);

            //var exerciseEditViewModel = new ExerciseEditViewModel(dialogService.Object);

            //var viewModel = new ExerciseCompositeViewModel(exerciseService.Object, exerciseSearchViewModel, exerciseEditViewModel, recorderViewModel);

            //exerciseEditViewModel.StartEdit(exercise);
            //exerciseEditViewModel.SaveCommand.Execute(null);

            //exerciseService.Verify(svc => svc.Add(It.IsAny<Exercise>()),
            //    Times.Once, "IExerciseService.Add was not called.");
            Assert.Fail("Need to put recorderViewModel back!!!!!!!!");

        }
    }

    



    //public class MockExerciseCompositeViewModel : ExerciseCompositeViewModel
    //{
    //    public bool EndEditinExerciseCalled = false;
    //    public EditorCloseOperation? EndEditOperation;
    //    public EntityLifeCycleState EndEditLifeCycleState;

    //    public MockExerciseCompositeViewModel(IExerciseService exerciseService, ExerciseManagementViewModel exerciseSearchViewModel,
    //        ExerciseEditViewModel exerciseEditViewModel,
    //        RecorderViewModel exerciseRecorderViewModel) : base(exerciseService, exerciseSearchViewModel, exerciseEditViewModel, exerciseRecorderViewModel)
    //    {

    //    }

    //    protected override void EndEditingExercise(IExercise exercise, EditorCloseOperation operation, EntityLifeCycleState entityLifeCycleState)
    //    {
    //        EndEditinExerciseCalled = true;
    //        EndEditOperation = operation;
    //        EndEditLifeCycleState = entityLifeCycleState;
    //    }
    //}
}
