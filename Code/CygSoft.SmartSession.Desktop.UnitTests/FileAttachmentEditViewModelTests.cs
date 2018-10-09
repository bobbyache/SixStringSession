using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class FileAttachmentEditViewModelTests
    {
        // https://stackoverflow.com/questions/7121867/how-can-i-validate-multiple-properties-when-any-of-them-changes

        // Thanks Vincent.  I meant that you could avoid using BindingGroups altogether by implementing IDataErrorInfo and setting the ValidatesOnDataError property of Binding to true.  
        // https://blogs.msdn.microsoft.com/vinsibal/2008/08/12/wpf-3-5-sp1-feature-bindinggroups-with-item-level-validation/
        [Test]
        public void FileAttachment_ChangeName_With_Empty_Path_Has_Consistent_State()
        {
            //Mock<IFileAttachmentService> service = new Mock<IFileAttachmentService>();
            //Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();
            //FileAttachmentEditViewModel viewModel = new FileAttachmentEditViewModel(service.Object, dialogService.Object);
            FileAttachment fileAttachment = new FileAttachment(@"C:\smartsession\files\attachment.txt", null);
            fileAttachment.ChangeName(null, "attachment_2");
            Assert.That(fileAttachment.Extension, Is.EqualTo(".txt"));
            Assert.That(fileAttachment.FileTitle, Is.EqualTo("attachment_2"));
            Assert.That(fileAttachment.FileName, Is.EqualTo("attachment_2.txt"));
        }

        //[Test]
        //public void FileAttachmentCreateViewModel_StartEdit_Is_Correctly_Initialized_And_Presented()
        //{
        //    Mock<IFileAttachmentService> service = new Mock<IFileAttachmentService>();
        //    Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

        //    FileAttachmentSearchViewModel searchViewModel = new FileAttachmentSearchViewModel(null, service.Object, dialogService.Object);

        //    var compositeViewModel = new FileAttachmentCompositeViewModel()

        //    //FileAttachmentEditViewModel viewModel = new FileAttachmentEditViewModel(service.Object, dialogService.Object);

        //    FileAttachment fileAttachment = new FileAttachment(@"C:\smartsession\files\attachment.txt", null);
        //    fileAttachment.ChangeName(null, "attachment_2");
        //    Assert.That(fileAttachment.Extension, Is.EqualTo(".txt"));
        //    Assert.That(fileAttachment.FileTitle, Is.EqualTo("attachment_2"));
        //    Assert.That(fileAttachment.FileName, Is.EqualTo("attachment_2.txt"));
        //}

    }
}
