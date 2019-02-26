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
    public class PracticeRoutineExerciseTests
    {
        [Test]
        public void PracticeRoutineExercise_AddMinutes_Assigns_Correct_Seconds()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.Minutes = 10;
            Assert.AreEqual(600, routineExercise.AssignedPracticeTime);
        }

        [Test]
        public void PracticeRoutineExercise_Assign_60_Seconds_As_1_Minute()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.AssignedPracticeTime = 60;
            Assert.AreEqual(1, routineExercise.Minutes);
        }

        [Test]
        public void PracticeRoutineExercise_Assign_90_Seconds_As_2_Minute()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.AssignedPracticeTime = 90;
            Assert.AreEqual(2, routineExercise.Minutes);
        }

        [Test]
        public void PracticeRoutineExercise_Assign_Less_Than_60_Seconds_As_1_Minute()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.AssignedPracticeTime = 59;
            Assert.AreEqual(1, routineExercise.Minutes);
        }

        [Test]
        public void PracticeRoutineExercise_Assign_0_Seconds_As_0_Minutes()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.AssignedPracticeTime = 0;
            Assert.AreEqual(0, routineExercise.Minutes);
        }

        [Test]
        public void PracticeRoutineExercise_Assign_2_Minutes_As_120_Seconds()
        {
            var routineExercise = GetRoutineExercise();

            routineExercise.Minutes = 2;
            Assert.AreEqual(120, routineExercise.AssignedPracticeTime);
            Assert.AreEqual(120, routineExercise.Seconds);
        }

        private PracticeRoutineExercise GetRoutineExercise()
        {
            var routineExercise = new PracticeRoutineExercise
            {
                AssignedPracticeTime = 300,
                Title = "Routine Exercise",
                FrequencyWeighting = 1,
                ExerciseId = 2,
                PracticeRoutineId = 3
            };
            return routineExercise;
        }
    }
}
