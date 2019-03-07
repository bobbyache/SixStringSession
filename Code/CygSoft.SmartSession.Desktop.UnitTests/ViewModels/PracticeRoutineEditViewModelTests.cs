using CygSoft.SmartSession.Desktop.PracticeRoutines;
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
        public void PracticeRoutineViewModel_StartEdit_Raises_PracticeRoutineTree_PropertyRaised_Event()
        {
            var fired = false;
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetEmptyPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "PracticeRoutineTree")
                    fired = true;
            };

            viewModel.StartEdit(practiceRoutine);

            Assert.IsTrue(fired);
        }

        [Test]
        public void PracticeRoutineViewModel_StartEdit__Populates_PracticeRoutineTree()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = EntityFactory.GetBasicPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            Assert.AreEqual("Practice Routine", viewModel.PracticeRoutineTree.Title);
            Assert.AreEqual(1, viewModel.PracticeRoutineTree.TimeSlots.Count);
            Assert.AreEqual(3, viewModel.PracticeRoutineTree.TimeSlots[0].Exercises.Count);
        }
    }
}
