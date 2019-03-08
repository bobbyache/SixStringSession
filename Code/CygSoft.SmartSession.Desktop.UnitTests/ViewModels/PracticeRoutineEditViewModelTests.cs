using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;
using CygSoft.SmartSession.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests.ViewModels
{
    [TestFixture]
    public class PracticeRoutineEditViewModelTests
    {
        [Test]
        public void Make_TreeviewItem_Work_in_MVVM_With_SelectedItem()
        {
            /* See: https://stackoverflow.com/questions/1000040/data-binding-to-selecteditem-in-a-wpf-treeview
             * Use an attached property to get this to work...
             * */
            Assert.Fail();
        }

        [Test]
        public void PracticeRoutineEditViewModel_AddTimeSlot()
        {
            // unit tests required...
            Assert.Fail();
        }

        [Test]
        public void PracticeRoutineEditViewModel_DeleteTimeSlot()
        {
            // unit tests required...
            Assert.Fail();
        }

        [Test]
        public void PracticeRoutineEditViewModel_Initialized_With_No_TimeSlot_Throws_Exception()
        {
            var dialogService = new Mock<IDialogViewService>();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            TestDelegate proc = () => viewModel.StartEdit(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void PracticeRoutineEditViewModel_Title_Is_Populated_When_Initialized()
        {
            var dialogService = new Mock<IDialogViewService>();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(EntityFactory.GetBasicPracticeRoutine());

            Assert.AreEqual("Practice Routine", viewModel.Title);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Title_Set_Populates_PracticeRoutine()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(practiceRoutine);

            viewModel.Title = "Modified Practice Routine";
            Assert.AreEqual("Modified Practice Routine", practiceRoutine.Title);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Title_Set_Fires_PropertyChanged()
        {
            var fired = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Title")
                    fired = true;
            };
            viewModel.Title = "Modified Practice Routine";

            Assert.IsTrue(fired);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Add_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            viewModel.AddTimeSlotCommand.Execute(null);

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Remove_TimeSlot_Actually_Removes_TimeSlot()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            TimeSlotViewModel firstTimeSlot = viewModel.TimeSlots[0];

            viewModel.SelectedTimeSlot = firstTimeSlot;

            var beforeCount = viewModel.TimeSlots.Count;
            viewModel.DeleteTimeSlotCommand.Execute(null);
            var afterCount = viewModel.TimeSlots.Count;

            Assert.AreEqual(beforeCount - 1, afterCount);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Remove_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            TimeSlotViewModel firstTimeSlot = viewModel.TimeSlots[0];
            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            viewModel.SelectedTimeSlot = firstTimeSlot;

            viewModel.DeleteTimeSlotCommand.Execute(null);

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Edit_TimeSlotViewModel_Raises_ItemChanged()
        {
            var listChanged = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            TimeSlotViewModel anyTimeSlot = viewModel.TimeSlots[0];

            viewModel.TimeSlots.ListChanged += (s, e) => listChanged = true;

            anyTimeSlot.Title = "Some Arbitrary New Title";

            Assert.IsTrue(listChanged);
        }

        [Test]
        public void PracticeRoutineEditViewModel_TimeSlots_Are_Populated_When_Initialized()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            Assert.AreEqual(1, viewModel.TimeSlots.Count);
            Assert.That(viewModel.TimeSlots[0], Is.TypeOf<TimeSlotViewModel>());
        }

        [Test]
        public void PracticeRoutineEditViewModel_When_TimeSlot_AssignedSeconds_Changes_Raises_PropertyChanged_TotalTimeDisplay()
        {
            var fired = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            var firstTimeSlot = viewModel.TimeSlots[0];

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "TotalTimeDisplay")
                    fired = true;
            };

            firstTimeSlot.AssignedSeconds = 600;

            Assert.IsTrue(fired);
        }

        [Test]
        public void PracticeRoutineViewModel_Assigned_A_PracticeRoutine_In_Constructor_Is_Not_Dirty()
        {
            var dialogService = new Mock<IDialogViewService>();

            var practiceRoutine = EntityFactory.CreateEmptyPracticeRoutine("New Practice Routine");
            practiceRoutine.Title = "My Test PracticeRoutine";

            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(practiceRoutine);
            Assert.That(viewModel.IsDirty, Is.False);
        }

        [Test]
        public void PracticeRoutineViewModel_Change_Title_Is_Now_Dirty()
        {
            var dialogService = new Mock<IDialogViewService>();

            var practiceRoutine = EntityFactory.GetEmptyPracticeRoutine();

            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(practiceRoutine);

            viewModel.Title = "Title has Changed";

            Assert.That(viewModel.IsDirty, Is.True);
        }

        [Test]
        public void PracticeRoutineViewModel_Assign_All_Fields_To_View_Ok()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetEmptyPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            Assert.AreEqual(EntityLifeCycleState.AsExistingEntity, viewModel.LifeCycleState);
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Existing Empty Practice Routine", viewModel.Title);
        }

        [Test]
        public void PracticeRoutineViewModel_Commits_All_Fields_Back_To_Domain_Object()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetEmptyPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            viewModel.Title = "Changed Title";

            viewModel.Commit();

            Assert.AreEqual(1, practiceRoutine.Id);
            Assert.AreEqual("Changed Title", practiceRoutine.Title);
        }

        [Test]
        public void PracticeRoutineViewModel_StartEdit__Populates_PracticeRoutineTree()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            Assert.AreEqual("Practice Routine", viewModel.Title);
            Assert.AreEqual(1, viewModel.TimeSlots.Count);
            Assert.AreEqual(3, viewModel.TimeSlots[0].Exercises.Count);
        }

        [Test]
        public void PracticeRoutineEditViewModel_When_TimeSlot_AssignedSeconds_Changes_Reflected_In_TotalTimeDisplay()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            var totalTimeBefore = viewModel.TotalTimeDisplay;
            var firstTimeSlot = viewModel.TimeSlots[0].AssignedSeconds = 600;
            var totalTimeAfter = viewModel.TotalTimeDisplay;

            Assert.AreEqual("00:05:00", totalTimeBefore);
            Assert.AreEqual("00:10:00", totalTimeAfter);
        }

        [Test]
        public void PracticeRoutineEditViewModel_Change_AssignedSeconds_Raises_PracticeRoutineTree_PropertyRaised_Event()
        {
            var fired = false;

            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "TotalTimeDisplay")
                    fired = true;
            };

            
            viewModel.TimeSlots[0].AssignedSeconds = 600;

            Assert.IsTrue(fired);
        }
    }
}
