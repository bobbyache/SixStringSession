using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class PracticeRoutineTests
    {
        [Test]
        public void PracticeRoutine_Successfully_Adds_Exercise()
        {
            var exercise = GetMetronomeExercise();
            var newPracticeRoutine = CreatePracticeRoutine();

            newPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
            {
                ExerciseId = exercise.Id,
                AssignedPracticeTime = 5000,
                DifficultyRating = 1,
                PracticalityRating = 3
            });

            var routineExercise = newPracticeRoutine.PracticeRoutineExercises[0];

            Assert.AreEqual(5000, routineExercise.AssignedPracticeTime);
            Assert.AreEqual(1, routineExercise.DifficultyRating);
            Assert.AreEqual(3, routineExercise.PracticalityRating);
        }

        private Exercise GetMetronomeExercise()
        {
            Exercise exercise = new Exercise
            {
                Id = 3,
                DateCreated = DateTime.Parse("2018-03-01 12:15:00"),
                DateModified = DateTime.Parse("2018-03-01 12:15:00"),
                TargetMetronomeSpeed = 150,
                TargetPracticeTime = 50000,
                PracticalityRating = 2,
                DifficultyRating = 3,
                Title = "Created Exercise Title"
            };

            return exercise;
        }


        private PracticeRoutine CreatePracticeRoutine()
        {
            return new PracticeRoutine()
            {
                Title = "New Practice Routine"
            };
        }
    }
}
