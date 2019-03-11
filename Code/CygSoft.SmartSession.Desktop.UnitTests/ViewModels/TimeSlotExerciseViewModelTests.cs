using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class TimeSlotExerciseViewModelTests
    {
        [Test]
        public void TimeSlotExerciseViewModel_Initialized_With_No_TimeSlotExercise_Throws_Exception()
        {
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);

            TestDelegate proc = () => new TimeSlotExerciseViewModel(null, timeSlotViewModel);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TimeSlotExerciseViewModel_Title_Is_Populated_When_Initialized()
        {
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
            TimeSlotExercise timeSlotExercise = EntityFactory.GetTimeSlotExercise();

            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
            Assert.AreEqual("Existing Exercise", viewModel.Title);
        }

        [Test]
        public void TimeSlotExerciseViewModel_FrequencyWeighting_Set_Fires_PropertyChanged()
        {
            var fired = false;
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
            TimeSlotExercise timeSlotExercise = EntityFactory.GetTimeSlotExercise();

            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FrequencyWeighting")
                    fired = true;
            };
            viewModel.FrequencyWeighting = 4;

            Assert.IsTrue(fired);
        }

        [Test]
        public void TimeSlotExerciseViewModel_Derives_From_NodeViewModel()
        {
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
            TimeSlotExercise timeSlotExercise = EntityFactory.GetTimeSlotExercise();

            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
            Assert.That(viewModel, Is.AssignableTo(typeof(TreeViewItemViewModel)));
        }

        [Test]
        public void TimeSlotExerciseViewModel_Exposes_Parent_TimeSlotViewModel()
        {
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
            TimeSlotExercise timeSlotExercise = EntityFactory.GetTimeSlotExercise();

            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
            Assert.IsNotNull(viewModel.TimeSlotViewModel);
            Assert.AreEqual("Existing TimeSlot Title", viewModel.TimeSlotViewModel.Title);
        }

        [Test]
        public void TimeSlotExerciseViewModel_Exposes_TimeSlotExercise()
        {
            PracticeRoutineTimeSlot timeSlot = EntityFactory.GetSingleTimeSlot();
            TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
            TimeSlotExercise timeSlotExercise = EntityFactory.GetTimeSlotExercise();

            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
            Assert.IsNotNull(viewModel.TimeSlotExercise);
            Assert.AreEqual(timeSlot[0].Title, viewModel.TimeSlotExercise.Title);
        }
    }
}
