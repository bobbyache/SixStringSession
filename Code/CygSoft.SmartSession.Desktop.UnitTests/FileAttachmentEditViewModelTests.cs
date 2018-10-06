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
            Assert.That(fileAttachment.GetFileName(), Is.EqualTo("attachment_2.txt"));
        }

    }
}
