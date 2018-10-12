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
    }
}
