using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using NUnit.Framework;
using System;

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
            });

            var routineExercise = newPracticeRoutine.PracticeRoutineExercises[0];

            Assert.AreEqual(5000, routineExercise.AssignedPracticeTime);
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
