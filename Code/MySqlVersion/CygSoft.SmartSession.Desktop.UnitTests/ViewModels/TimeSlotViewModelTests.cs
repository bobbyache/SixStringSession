using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Edit;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class TimeSlotViewModelTests
    {
        [Test]
        public void TimeSlotViewModel_Initialized_With_No_TimeSlot_Throws_Exception()
        {
            TestDelegate proc = () => new TimeSlotViewModel(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TimeSlotViewModel_Title_Reflects_Initialized_State()
        {
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);

            Assert.AreEqual(timeSlot.Title, viewModel.Title);
        }


        [Test]
        public void TimeSlotViewModel_DisplayTime_Reflects_Initialized_State()
        {
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);

            Assert.AreEqual("00:05:00", viewModel.DisplayTime);
        }


        [Test]
        public void TimeSlotViewModel_Title_Set_Fires_PropertyChanged()
        {
            var fired = false;
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Title")
                    fired = true;
            };
            viewModel.Title = "Modified Title";

            Assert.IsTrue(fired);
        }

        [Test]
        public void TimeSlotViewModel_AssignedSeconds_Reflects_Initialized_State()
        {
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);

            Assert.AreEqual(timeSlot.AssignedSeconds, viewModel.AssignedSeconds);
        }

        [Test]
        public void TimeSlotViewModel_AssignedSeconds_Set_Fires_PropertyChanged()
        {
            var fired = false;
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "AssignedSeconds")
                    fired = true;
            };
            viewModel.AssignedSeconds = 600;

            Assert.IsTrue(fired);
        }

        [Test]
        public void TimeSlotViewModel_AssignedSeconds_Set_Fires_PropertyChanged_For_DisplayTime()
        {
            var fired = false;
            var timeSlot = GetBasicTimeSlot();
            var viewModel = new TimeSlotViewModel(timeSlot);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "DisplayTime")
                    fired = true;
            };
            viewModel.AssignedSeconds = 600;

            Assert.IsTrue(fired);
        }

        [Test]
        public void TimeSlotViewModel_Remove_TimeSlot_Actually_Removes_TimeSlot()
        {
            TimeSlotViewModel viewModel = new TimeSlotViewModel(GetBasicTimeSlot());
            TimeSlotExerciseViewModel first = viewModel.Exercises[0];

            viewModel.SelectedExercise = first;

            var beforeCount = viewModel.Exercises.Count;
            viewModel.Exercises.Remove(first);
            var afterCount = viewModel.Exercises.Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }

        [Test]
        public void TimeSlotViewModel_Remove_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;
            TimeSlotViewModel viewModel = new TimeSlotViewModel(GetBasicTimeSlot());
            TimeSlotExerciseViewModel first = viewModel.Exercises[0];
            viewModel.Exercises.ListChanged += (s, e) => listChanged = true;

            viewModel.SelectedExercise = first;

            viewModel.Exercises.Remove(first);

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void TimeSlotViewModel_Edit_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;
            TimeSlotViewModel viewModel = new TimeSlotViewModel(GetBasicTimeSlot());
            TimeSlotExerciseViewModel anyExercise = viewModel.Exercises[0];

            viewModel.Exercises.ListChanged += (s, e) => listChanged = true;

            anyExercise.FrequencyWeighting = 1;

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void TimeSlotViewModel_TimeSlots_Are_Populated_When_Initialized()
        {
            TimeSlotViewModel viewModel = new TimeSlotViewModel(GetBasicTimeSlot());
            Assert.AreEqual(3, viewModel.Exercises.Count);
            Assert.That(viewModel.Exercises[0], Is.TypeOf<TimeSlotExerciseViewModel>());
        }

        [Test]
        public void TimeSlotViewModel_IncrementMinutes_Reflects_On_Domain()
        {
            var timeSlot = GetBasicTimeSlot();
            TimeSlotViewModel viewModel = new TimeSlotViewModel(timeSlot);

            var timeBefore = timeSlot.AssignedSeconds;
            viewModel.IncrementMinutesCommand.Execute(null);
            var timeAfter = timeSlot.AssignedSeconds;

            Assert.AreEqual(timeAfter, timeBefore + 60);
        }

        [Test]
        public void TimeSlotViewModel_DecrementtMinutes_Reflects_On_Domain()
        {
            var timeSlot = GetBasicTimeSlot();
            TimeSlotViewModel viewModel = new TimeSlotViewModel(timeSlot);

            var timeBefore = timeSlot.AssignedSeconds;
            viewModel.DecrementMinutesCommand.Execute(null);
            var timeAfter = timeSlot.AssignedSeconds;

            Assert.AreEqual(timeAfter, timeBefore - 60);
        }

        [Test]
        public void TimeSlotViewModel_DecrementtMinutes_Is_Less_Than_Zero_Is_Zero()
        {
            var timeSlot = GetBasicTimeSlot();
            timeSlot.AssignedSeconds = 60;

            TimeSlotViewModel viewModel = new TimeSlotViewModel(timeSlot);

            viewModel.DecrementMinutesCommand.Execute(null);
            viewModel.DecrementMinutesCommand.Execute(null);

            Assert.AreEqual(0, timeSlot.AssignedSeconds);
        }

        private PracticeRoutineTimeSlot GetBasicTimeSlot()
        {
            List<TimeSlotExercise> timeSlotExercises = new List<TimeSlotExercise>
            {
                new TimeSlotExercise(1, 1, "Exercise 1", 3),
                new TimeSlotExercise(2, 1, "Exercise 2", 3),
                new TimeSlotExercise(3, 1, "Exercise 3", 3)
            };

            return new PracticeRoutineTimeSlot(1, "Time Slot", 300, timeSlotExercises);
        }
    }
}
