using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight.Messaging;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class FileAttachmentCompositeViewModelTests
    {
        [Test]
        public void FileAttachmentUpdateViewModel_StartEdit_Calls_Get_On_FileAttachmentService()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            fileAttachmentService.Setup(s => s.Get(It.IsAny<int>()))
            .Returns(new FileAttachment
            {
                Id = 2,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Extension = ".txt",
                FileTitle = "file_title",
                Notes = "Here are some notes."
            });

            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            var updateViewModel = new FileAttachmentUpdateViewModel(fileAttachmentService.Object, dialogService.Object);

            updateViewModel.StartEdit(2);

            fileAttachmentService.Verify(s => s.Get(It.IsAny<int>()), Times.Once, "FileAttachmentService must call Get() when StartEdit() invoked.");
        }

        [Test]
        public void FileAttachmentCompositeViewModel_When_Sent_Create_Message_Sets_Create_View()
        {
            var compositeViewModel = InitializeNewCompositeViewModel();

            // no search result selected...
            Messenger.Default.Send(new StartEditingFileAttachmentMessage(null, StartEditingEntityMode.Create));

            Assert.That(compositeViewModel.CurrentViewModel, Is.TypeOf(typeof(FileAttachmentCreateViewModel)));
        }

        [Test]
        public void FileAttachmentCompositeViewModel_When_Sent_Update_Message_Sets_Update_View()
        {
            var compositeViewModel = InitializeNewCompositeViewModel();

            FileAttachmentSearchResultModel searchResultModel = new FileAttachmentSearchResultModel();
            searchResultModel.Id = 3;
            searchResultModel.Extension = ".txt";
            searchResultModel.Notes = "Some notes...";
            searchResultModel.FileTitle = "file_title";

            // no search result selected...
            Messenger.Default.Send(new StartEditingFileAttachmentMessage(searchResultModel, StartEditingEntityMode.Update));

            Assert.That(compositeViewModel.CurrentViewModel, Is.TypeOf(typeof(FileAttachmentUpdateViewModel)));
        }

        private FileAttachmentCompositeViewModel InitializeNewCompositeViewModel()
        {
            var fileAttachmentService = new Mock<IFileAttachmentService>();
            fileAttachmentService.Setup(s => s.Get(It.IsAny<int>()))
            .Returns(new FileAttachment
            {
                Id = 2,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Extension = ".txt",
                FileTitle = "file_title",
                Notes = "Here are some notes."
            }); // required for when we call Get() from the service... can't edit a NULL FileAttachment.

            var dialogService = new Mock<IDialogViewService>();

            var searchCriteriaViewModel = new FileAttachmentSearchCriteriaViewModel(fileAttachmentService.Object, dialogService.Object);
            var searchViewModel = new FileAttachmentSearchViewModel(searchCriteriaViewModel, fileAttachmentService.Object, dialogService.Object);
            var createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            var updateViewModel = new FileAttachmentUpdateViewModel(fileAttachmentService.Object, dialogService.Object);

            return new FileAttachmentCompositeViewModel(searchViewModel, createViewModel, updateViewModel);
        }
    }
}
