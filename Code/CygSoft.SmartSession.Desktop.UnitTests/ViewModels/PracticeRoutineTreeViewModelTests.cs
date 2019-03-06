using CygSoft.SmartSession.Desktop.PracticeRoutines;
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
    public class PracticeRoutineTreeViewModelTests
    {
        [Test]
        public void PracticeRoutineTreeViewModel_Initialized_With_No_TimeSlot_Throws_Exception()
        {
            TestDelegate proc = () => new PracticeRoutineTreeViewModel(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Title_Is_Populated_When_Initialized()
        {
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());
            Assert.AreEqual("Practice Routine", viewModel.Title);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Title_Set_Populates_PracticeRoutine()
        {
            var practiceRoutine = GetBasicPracticeRoutine();
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(practiceRoutine);
            viewModel.Title = "Modified Practice Routine";
            Assert.AreEqual("Modified Practice Routine", practiceRoutine.Title);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Title_Set_Fires_PropertyChanged()
        {
            var fired = false;
            var practiceRoutine = GetBasicPracticeRoutine();
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(practiceRoutine);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Title")
                    fired = true;
            };
            viewModel.Title = "Modified Practice Routine";

            Assert.IsTrue(fired);
        }
        
        [Test]
        public void PracticeRoutineTreeViewModel_Add_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());

            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            viewModel.AddCommand.Execute(null);

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Remove_TimeSlot_Actually_Removes_TimeSlot()
        {
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());
            TimeSlotViewModel firstTimeSlot = viewModel.TimeSlots[0];

            viewModel.SelectedTimeSlot = firstTimeSlot;

            var beforeCount = viewModel.TimeSlots.Count;
            viewModel.RemoveSelectionCommand.Execute(null);
            var afterCount = viewModel.TimeSlots.Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Remove_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());
            TimeSlotViewModel firstTimeSlot = viewModel.TimeSlots[0];
            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            viewModel.SelectedTimeSlot = firstTimeSlot;

            viewModel.RemoveSelectionCommand.Execute(null);

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_Edit_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());
            TimeSlotViewModel anyTimeSlot = viewModel.TimeSlots[0];

            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            anyTimeSlot.Title = "Some Arbitrary New Title";

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineTreeViewModel_TimeSlots_Are_Populated_When_Initialized()
        {
            PracticeRoutineTreeViewModel viewModel = new PracticeRoutineTreeViewModel(GetBasicPracticeRoutine());
            Assert.AreEqual(1, viewModel.TimeSlots.Count);
            Assert.That(viewModel.TimeSlots[0], Is.TypeOf<TimeSlotViewModel>());
        }

        private PracticeRoutine GetBasicPracticeRoutine()
        {
            List<TimeSlotExercise> timeSlotExercises = new List<TimeSlotExercise>
            {
                new TimeSlotExercise(1, 1, "Exercise 1", 3),
                new TimeSlotExercise(2, 1, "Exercise 2", 3),
                new TimeSlotExercise(3, 1, "Exercise 3", 3)
            };

            List<PracticeRoutineTimeSlot> timeSlots = new List<PracticeRoutineTimeSlot>
            {
                new PracticeRoutineTimeSlot(1, "Time Slot 1", 10, timeSlotExercises)
            };

            return new PracticeRoutine(1, "Practice Routine", timeSlots);
        }
    }
}
