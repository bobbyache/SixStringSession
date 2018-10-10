using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class FileAttachmentServiceTests
    {
        [Test]
        public void FileAttachmentService_Add_FileAttachment_With_No_SourceFile_ThrowsException()
        {
            string fileTitle = " ";

            var fileAttachment = new FileAttachment();
            fileAttachment.SourceFilePath = null;

            Mock<IFileService> fileService = new Mock<IFileService>();
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
            FileAttachmentService fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            TestDelegate testDelegate = () => fileAttachmentService.Add(fileAttachment);
            Assert.That(testDelegate, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void FileAttachmentService_Add_FileAttachment_With_FileName_That_Matches_Existing_ThrowsException()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var fileAttachment = new FileAttachment(@"C:\SomeOtherFolder\Files\new_file.gp", null);

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true); // condition mocked...

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            TestDelegate testDelegate = () => fileAttachmentService.Add(fileAttachment);
            Assert.That(testDelegate, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void FileAttachmentService_Add_FileAttachment_Successfully_Generates_FileId()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");
            fileService.Setup(s => s.GenerateFileId()).Returns(@"27098039-5725-4564-92FD-2F222621D688");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            var fileAttachment = new FileAttachment(@"C:\SomeOtherFolder\Files\new_file.gp", null);
            fileAttachmentService.Add(fileAttachment);

            fileService.Verify(fService => fService.GenerateFileId(), Times.Once, "Copy was not called.");
        }

        [Test]
        public void FileAttachmentService_Add_FileAttachment_Successfully_Invokes_Copy_Method()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");
            fileService.Setup(s => s.GenerateFileId()).Returns(@"27098039-5725-4564-92FD-2F222621D688");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            var fileAttachment = new FileAttachment(@"C:\SomeOtherFolder\Files\new_file.gp", null);
            fileAttachmentService.Add(fileAttachment);

            fileService.Verify(fService => fService.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Once, "Copy was not called.");
        }

        [Test]
        public void FileAttachmentService_Remove_FileAttachment_Successfully_Invokes_Delete_Method_OnFileService()
        {
            var repository = new Mock<IFileAttachmentRepository>();
            repository.Setup(r => r.Get(It.IsAny<int>())).Returns(new FileAttachment
            {
                Id = 5,
                Title = "file_title",
                Extension = ".txt"
            });

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(repository.Object);

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);
            
            fileAttachmentService.Remove(5);

            fileService.Verify(fService => fService.Delete(It.IsAny<string>()), Times.Once, "Delete was not called.");
        }

        [Test]
        public void FileAttachmentService_Update_FileAttachment_With_SourceFile_DoesNot_Invoke_Copy()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileAttachment = new FileAttachment(@"C:\SomeOtherFolder\Files\new_file.gp", null);
            fileAttachment.Id = 23;
            fileAttachment.FileId = @"27098039-5725-4564-92FD-2F222621D688";

            var fileAttachmentsRepository = new Mock<IFileAttachmentRepository>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");
            fileService.Setup(s => s.GenerateFileId()).Returns(@"27098039-5725-4564-92FD-2F222621D688");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            fileAttachmentService.Update(fileAttachment);
            fileService.Verify(fService => fService.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Never, "Copy was not called.");
        }

        [Test]
        public void FileAttachmentService_Update_FileAttachment_With_No_SourceFile_DoesNot_Invoke_Copy()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileAttachment = new FileAttachment(null, "NewFileName.gp");
            fileAttachment.Id = 23;

            var fileAttachmentsRepository = new Mock<IFileAttachmentRepository>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            fileAttachmentService.Update(fileAttachment);
            fileService.Verify(fService => fService.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Never, "Copy was called, but shouldn't have.");
        }
    }
}
