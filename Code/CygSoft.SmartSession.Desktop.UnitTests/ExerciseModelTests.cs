using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Domain.Exercises;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class ExerciseModelTests
    {
        [Test]
        public void ExerciseModel_Assigned_An_Exercise_In_Constructor_Is_Not_Dirty()
        {
            var exercise = new Exercise();
            exercise.Title = "My Test Exercise";
            exercise.Notes = "Some notes.";

            var exerciseModel = new ExerciseModel(exercise);

            Assert.That(exerciseModel.IsDirty, Is.False);
        }

        [Test]
        public void ExerciseModel_Change_Title_Is_Now_Dirty()
        {
            var exercise = new Exercise();
            exercise.Title = "My Test Exercise";
            exercise.Notes = "Some notes.";

            var exerciseModel = new ExerciseModel(exercise);
            exerciseModel.Title = "Title has Changed";

            Assert.That(exerciseModel.IsDirty, Is.True);
        }

        [Test]
        public void ExerciseModel_Assign_All_Fields_To_View_Ok()
        {
            var exercise = new Exercise();
            exercise.DateCreated = DateTime.Parse("2018/07/03");
            exercise.DateModified = DateTime.Parse("2018/07/03");
            exercise.Id = 2;
            exercise.Title = "Title";
            exercise.Notes = "Some notes";
            exercise.DifficultyRating = 3;
            exercise.OptimalDuration = 2000;
            exercise.PracticalityRating = 4;
            exercise.TargetMetronomeSpeed = 120;
            exercise.TargetPracticeTime = 15000;
            exercise.Weighting = 800;

            var exerciseModel = new ExerciseModel(exercise);

            Assert.AreEqual(2, exerciseModel.Id);
            Assert.AreEqual("Title", exerciseModel.Title);
            Assert.AreEqual("Some notes", exerciseModel.Notes);
            Assert.AreEqual(3, exerciseModel.DifficultyRating);
            Assert.AreEqual(2000, exerciseModel.OptimalDuration);
            Assert.AreEqual(4, exerciseModel.PracticalityRating);
            Assert.AreEqual(120, exerciseModel.TargetMetronomeSpeed);
            Assert.AreEqual(15000, exerciseModel.TargetPracticeTime);
            Assert.AreEqual(800, exerciseModel.Weighting);
        }

        [Test]
        public void ExerciseModel_Commits_All_Fields_Back_To_Domain_Object()
        {
            var exercise = new Exercise();
            exercise.DateCreated = DateTime.Parse("2018/07/03");
            exercise.DateModified = DateTime.Parse("2018/07/03");
            exercise.Id = 2;
            exercise.Title = "Title";
            exercise.Notes = "Some notes";
            exercise.DifficultyRating = 3;
            exercise.OptimalDuration = 2000;
            exercise.PracticalityRating = 4;
            exercise.TargetMetronomeSpeed = 120;
            exercise.TargetPracticeTime = 15000;
            exercise.Weighting = 800;

            var exerciseModel = new ExerciseModel(exercise);

            exerciseModel.Title = "Changed Title";
            exerciseModel.Notes = "Some more notes";
            exerciseModel.Weighting = 300;
            exerciseModel.DifficultyRating = 1;
            exerciseModel.OptimalDuration = 3000;
            exerciseModel.PracticalityRating = 1;
            exerciseModel.TargetMetronomeSpeed = 100;
            exerciseModel.TargetPracticeTime = 15000;

            exerciseModel.Commit();

            Assert.AreEqual(2, exercise.Id);
            Assert.AreEqual("Changed Title", exercise.Title);
            Assert.AreEqual("Some more notes", exercise.Notes);
            Assert.AreEqual(1, exercise.DifficultyRating);
            Assert.AreEqual(3000, exercise.OptimalDuration);
            Assert.AreEqual(1, exercise.PracticalityRating);
            Assert.AreEqual(100, exercise.TargetMetronomeSpeed);
            Assert.AreEqual(15000, exercise.TargetPracticeTime);
            Assert.AreEqual(300, exercise.Weighting);
        }
    }
}
