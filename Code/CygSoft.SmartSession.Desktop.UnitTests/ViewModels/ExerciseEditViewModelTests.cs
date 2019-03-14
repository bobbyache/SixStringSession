using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class ExerciseEditViewModelTests
    {
        [Test]
        public void ExerciseViewModel_Assigned_An_Exercise_In_Constructor_Is_Not_Dirty()
        {
            var dialogService = new Mock<IDialogViewService>();

            var exercise = new Exercise();
            exercise.Title = "My Test Exercise";

            var viewModel = new ExerciseEditViewModel(dialogService.Object);
            viewModel.StartEdit(exercise);
            Assert.That(viewModel.IsDirty, Is.False);
        }

        [Test]
        public void ExerciseViewModel_Change_Title_Is_Now_Dirty()
        {
            var dialogService = new Mock<IDialogViewService>();

            var exercise = GetExistingTestExercise();

            var viewModel = new ExerciseEditViewModel(dialogService.Object);
            viewModel.StartEdit(exercise);

            viewModel.Title = "Title has Changed";

            Assert.That(viewModel.IsDirty, Is.True);
        }

        [Test]
        public void ExerciseViewModel_Assign_All_Fields_To_View_Ok()
        {
            var dialogService = new Mock<IDialogViewService>();
            var exercise = GetExistingTestExercise();
            var viewModel = new ExerciseEditViewModel(dialogService.Object);

            viewModel.StartEdit(exercise);

            Assert.AreEqual(EntityLifeCycleState.AsExistingEntity, viewModel.LifeCycleState);
            Assert.AreEqual(2, viewModel.Id);
            Assert.AreEqual("Title", viewModel.Title);
            Assert.AreEqual(120, viewModel.TargetMetronomeSpeed);
            Assert.AreEqual(15000, viewModel.TargetPracticeTime);
            Assert.AreEqual(50, viewModel.SpeedProgressWeighting);
            Assert.AreEqual(50, viewModel.PracticeTimeProgressWeighting);
        }

        [Test]
        public void ExerciseViewModel_Commits_All_Fields_Back_To_Domain_Object()
        {
            var dialogService = new Mock<IDialogViewService>();
            var exercise = GetExistingTestExercise();
            var viewModel = new ExerciseEditViewModel(dialogService.Object);

            viewModel.StartEdit(exercise);

            viewModel.Title = "Changed Title";
            viewModel.TargetMetronomeSpeed = 100;
            viewModel.TargetPracticeTime = 15000;
            viewModel.PracticeTimeProgressWeighting = 30;
            viewModel.SpeedProgressWeighting = 70;

            viewModel.Commit();

            Assert.AreEqual(2, exercise.Id);
            Assert.AreEqual("Changed Title", exercise.Title);
            Assert.AreEqual(100, exercise.TargetMetronomeSpeed);
            Assert.AreEqual(15000, exercise.TargetPracticeTime);
            Assert.AreEqual(30, exercise.PracticeTimeProgressWeighting);
            Assert.AreEqual(70, exercise.SpeedProgressWeighting);
        }

        private Exercise GetExistingTestExercise()
        {
            var exercise = new Domain.Exercises.Exercise();
            exercise.DateCreated = DateTime.Parse("2018/07/03");
            exercise.DateModified = DateTime.Parse("2018/07/03");
            exercise.Id = 2;
            exercise.Title = "Title";
            exercise.TargetMetronomeSpeed = 120;
            exercise.TargetPracticeTime = 15000;

            return exercise;
        }
    }
}
