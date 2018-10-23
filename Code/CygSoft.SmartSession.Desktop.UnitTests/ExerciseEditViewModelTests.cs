using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class ExerciseEditViewModelTests
    {
        [TestFixture]
        public class ExerciseModelTests
        {
            [Test]
            public void ExerciseModel_Assigned_An_Exercise_In_Constructor_Is_Not_Dirty()
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();

                var exercise = new Exercise();
                exercise.Title = "My Test Exercise";

                var exerciseModel = new ExerciseEditViewModel(exerciseService.Object, dialogService.Object);
                Assert.That(exerciseModel.IsDirty, Is.False);
            }

            [Test]
            public void ExerciseModel_Change_Title_Is_Now_Dirty()
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();

                var exercise = GetExistingTestExercise();
                var exerciseSearchResult = GetExistingTestExerciseSearchResult();

                exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                    .Returns(exercise);

                var exerciseModel = new ExerciseEditViewModel(exerciseService.Object, dialogService.Object);
                exerciseModel.StartEdit(exerciseSearchResult);

                exerciseModel.Title = "Title has Changed";

                Assert.That(exerciseModel.IsDirty, Is.True);
            }

            [Test]
            public void ExerciseModel_Assign_All_Fields_To_View_Ok()
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();

                var exercise = GetExistingTestExercise();
                var exerciseSearchResult = GetExistingTestExerciseSearchResult();

                exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                    .Returns(exercise);

                var exerciseModel = new ExerciseEditViewModel(exerciseService.Object, dialogService.Object);

                exerciseModel.StartEdit(exerciseSearchResult);

                Assert.AreEqual(2, exerciseModel.Id);
                Assert.AreEqual("Title", exerciseModel.Title);
                Assert.AreEqual(3, exerciseModel.DifficultyRating);
                Assert.AreEqual(4, exerciseModel.PracticalityRating);
                Assert.AreEqual(120, exerciseModel.TargetMetronomeSpeed);
                Assert.AreEqual(15000, exerciseModel.TargetPracticeTime);
            }

            [Test]
            public void ExerciseModel_Commits_All_Fields_Back_To_Domain_Object()
            {
                var exerciseService = new Mock<IExerciseService>();
                var dialogService = new Mock<IDialogViewService>();

                var exercise = GetExistingTestExercise();
                var exerciseSearchResult = GetExistingTestExerciseSearchResult();

                exerciseService.Setup(srv => srv.Get(It.IsAny<int>()))
                    .Returns(exercise);

                var exerciseModel = new ExerciseEditViewModel(exerciseService.Object, dialogService.Object);
                exerciseModel.StartEdit(exerciseSearchResult);

                exerciseModel.Title = "Changed Title";
                exerciseModel.DifficultyRating = 1;
                exerciseModel.PracticalityRating = 1;
                exerciseModel.TargetMetronomeSpeed = 100;
                exerciseModel.TargetPracticeTime = 15000;

                exerciseModel.Commit();

                Assert.AreEqual(2, exercise.Id);
                Assert.AreEqual("Changed Title", exercise.Title);
                Assert.AreEqual(1, exercise.DifficultyRating);
                Assert.AreEqual(1, exercise.PracticalityRating);
                Assert.AreEqual(100, exercise.TargetMetronomeSpeed);
                Assert.AreEqual(15000, exercise.TargetPracticeTime);
            }

            private Exercise GetExistingTestExercise()
            {
                var exercise = new Exercise();
                exercise.DateCreated = DateTime.Parse("2018/07/03");
                exercise.DateModified = DateTime.Parse("2018/07/03");
                exercise.Id = 2;
                exercise.Title = "Title";
                exercise.DifficultyRating = 3;
                exercise.PracticalityRating = 4;
                exercise.TargetMetronomeSpeed = 120;
                exercise.TargetPracticeTime = 15000;

                return exercise;
            }

            private ExerciseSearchResultModel GetExistingTestExerciseSearchResult()
            {
                return new ExerciseSearchResultModel()
                {
                    Id = 2,
                    Title = "Title",
                    DifficultyRating = 3,
                    PracticalityRating = 4
                };
            }
        }
    }
}
