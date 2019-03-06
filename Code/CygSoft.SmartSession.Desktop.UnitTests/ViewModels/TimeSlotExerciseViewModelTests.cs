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
    public class TimeSlotExerciseViewModelTests
    {
        [Test]
        public void TimeSlotExerciseViewModel_Initialized_With_No_TimeSlot_Throws_Exception()
        {
            TestDelegate proc = () => new TimeSlotExerciseViewModel(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TimeSlotExerciseViewModel_Title_Is_Populated_When_Initialized()
        {
            TimeSlotExercise timeSlotExercise = new TimeSlotExercise(1, 1, "Exercise", 3);
            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise);
            Assert.AreEqual("Exercise", viewModel.Title);
        }

        [Test]
        public void TimeSlotExerciseViewModel_FrequencyWeighting_Set_Fires_PropertyChanged()
        {
            var fired = false;
            TimeSlotExercise timeSlotExercise = new TimeSlotExercise(1, 1, "Exercise", 3);
            TimeSlotExerciseViewModel viewModel = new TimeSlotExerciseViewModel(timeSlotExercise);
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "FrequencyWeighting")
                    fired = true;
            };
            viewModel.FrequencyWeighting = 4;

            Assert.IsTrue(fired);
        }
    }
}
