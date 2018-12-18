using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;
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

            var practiceRoutine = new PracticeRoutine();
            practiceRoutine.Title = "My Test PracticeRoutine";

            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(practiceRoutine);
            Assert.That(viewModel.IsDirty, Is.False);
        }

        [Test]
        public void PracticeRoutineViewModel_Change_Title_Is_Now_Dirty()
        {
            var dialogService = new Mock<IDialogViewService>();

            var practiceRoutine = GetExistingTestPracticeRoutine();

            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);
            viewModel.StartEdit(practiceRoutine);

            viewModel.Title = "Title has Changed";

            Assert.That(viewModel.IsDirty, Is.True);
        }

        [Test]
        public void PracticeRoutineViewModel_Assign_All_Fields_To_View_Ok()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = GetExistingTestPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            Assert.AreEqual(EntityLifeCycleState.AsExistingEntity, viewModel.LifeCycleState);
            Assert.AreEqual(2, viewModel.Id);
            Assert.AreEqual("Title", viewModel.Title);;
        }

        [Test]
        public void PracticeRoutineViewModel_Commits_All_Fields_Back_To_Domain_Object()
        {
            var dialogService = new Mock<IDialogViewService>();
            var practiceRoutine = GetExistingTestPracticeRoutine();
            var viewModel = new PracticeRoutineEditViewModel(dialogService.Object);

            viewModel.StartEdit(practiceRoutine);

            viewModel.Title = "Changed Title";

            viewModel.Commit();

            Assert.AreEqual(2, practiceRoutine.Id);
            Assert.AreEqual("Changed Title", practiceRoutine.Title);
        }

        private PracticeRoutine GetExistingTestPracticeRoutine()
        {
            var practiceRoutine = new Domain.PracticeRoutines.PracticeRoutine();
            practiceRoutine.DateCreated = DateTime.Parse("2018/07/03");
            practiceRoutine.DateModified = DateTime.Parse("2018/07/03");
            practiceRoutine.Id = 2;
            practiceRoutine.Title = "Title";

            return practiceRoutine;
        }
    }
}
