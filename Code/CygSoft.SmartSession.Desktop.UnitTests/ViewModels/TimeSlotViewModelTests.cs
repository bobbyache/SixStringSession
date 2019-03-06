using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;
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
