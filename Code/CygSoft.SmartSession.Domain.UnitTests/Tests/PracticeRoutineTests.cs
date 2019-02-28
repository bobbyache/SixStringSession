using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
        public void PracticeRoutine_New_Instantiate_Successfully_Injects_State()
        {
            var timeSlots = EntityFactory.CreateSingleTimeSlotList();
            var practiceRoutine = new PracticeRoutine("Test Title", timeSlots);

            Assert.AreEqual(1, timeSlots[0].Exercises.Count());
            Assert.AreEqual(1, practiceRoutine.TimeSlotCount);
            Assert.AreEqual("Test Title", practiceRoutine.Title);
            Assert.That(practiceRoutine.Id, Is.Zero);
        }

        [Test]
        public void PracticeRoutine_Existing_Instantiate_Successfully_Injects_State()
        {
            var timeSlots = EntityFactory.CreateSingleTimeSlotList();

            var practiceRoutine = new PracticeRoutine(1, "Test Title", timeSlots);

            Assert.AreEqual(1, timeSlots[0].Exercises.Count());
            Assert.AreEqual(1, practiceRoutine.TimeSlotCount);
            Assert.AreEqual("Test Title", practiceRoutine.Title);
            Assert.That(practiceRoutine.Id, Is.Not.Zero);
        }

        [Test]
        public void PracticeRoutine_New_Constructor_Should_Not_Take_Null_ExerciseList()
        {
            ActualValueDelegate<object> nullConstructor = () => new PracticeRoutine("Title", null);
            Assert.That(nullConstructor, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutine_Existing_Constructor_Should_Not_Take_Null_ExerciseList()
        {
            ActualValueDelegate<object> nullConstructor = () => new PracticeRoutine(1, "Title", null);
            Assert.That(nullConstructor, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutine_Successfully_Adds_Exercise()
        {
            var exercise = GetMetronomeExercise();
            var newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine();

            newPracticeRoutine.PracticeRoutineExercises.Add(new PracticeRoutineExercise
            {
                ExerciseId = exercise.Id,
                AssignedPracticeTime = 5000,
                FrequencyWeighting = 3
            });

            var routineExercise = newPracticeRoutine.PracticeRoutineExercises[0];

            Assert.AreEqual(5000, routineExercise.AssignedPracticeTime);
            Assert.AreEqual(3, routineExercise.FrequencyWeighting);
        }

        [Test]
        public void PracticeRoutine_Removes_A_TimeSlot_Successfully()
        {
            var newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine();

            var timeSlot = EntityFactory.CreateSingleTimeSlot();
            newPracticeRoutine.AddTimeSlot(timeSlot);

            newPracticeRoutine.RemoveTimeSlot(timeSlot);

            Assert.AreEqual(0, newPracticeRoutine.TimeSlotCount);
        }

        [Test]
        public void PracticeRoutine_Adds_A_TimeSlot_Successfully()
        {
            var newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine();

            var timeSlot = EntityFactory.CreateSingleTimeSlot();
            newPracticeRoutine.AddTimeSlot(timeSlot);

            Assert.AreEqual(1, newPracticeRoutine.TimeSlotCount);
        }

        [Test]
        public void PracticeRoutine_Attempt_To_Add_Duplicate_TimeSlot_()
        {
            var newPracticeRoutine = EntityFactory.CreateEmptyPracticeRoutine();

            var timeSlot = EntityFactory.CreateSingleTimeSlot();
            newPracticeRoutine.AddTimeSlot(timeSlot);
            var duplicate = EntityFactory.CreateSingleTimeSlot();

            TestDelegate testDelegate = () => newPracticeRoutine.AddTimeSlot(duplicate);
            Assert.That(testDelegate, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void PracticeRoutine_DoNot_Allow_Adding_Existing_Database_TimeSlot()
        {
            var newPracticeRoutine = EntityFactory.GetEmptyPracticeRoutine();
            var newTimeSlot = EntityFactory.CreateSingleTimeSlot("New Time Slot");
            var existingTimeSlot = EntityFactory.GetSingleTimeSlot("Database (Existing) Time Slot");

            newPracticeRoutine.AddTimeSlot(newTimeSlot);

            TestDelegate testDelegate = () => newPracticeRoutine.AddTimeSlot(existingTimeSlot);
            Assert.That(testDelegate, Throws.TypeOf<ArgumentException>());
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
    }
}
