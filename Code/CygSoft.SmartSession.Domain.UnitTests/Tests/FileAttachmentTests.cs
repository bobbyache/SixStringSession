using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Common;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class FileAttachmentTests
    {
        [Test]
        public void FileAttachment_Constructor_With_ModifiedTitle_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var fileTitle = "new_file_modified_name";

            var fileAttachment = new FileAttachment(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file_modified_name"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_ModifiedTitle_With_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var fileTitle = "new_file_modified_name.gp";

            var fileAttachment = new FileAttachment(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file_modified_name"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_Null_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string fileTitle = null;

            var fileAttachment = new FileAttachment(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_Empty_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string fileTitle = " ";

            var fileAttachment = new FileAttachment(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_ChangeName_With_ModifiedTitle_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var fileTitle = "new_file_modified_name";

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file_modified_name"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_ChangeName_With_ModifiedTitle_With_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var fileTitle = "new_file_modified_name.gp";

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file_modified_name"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_ChangeName_With_Null_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string fileTitle = null;

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_ChangeName_With_Empty_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string fileTitle = " ";

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(filePath, fileTitle);

            Assert.That(fileAttachment.FileTitle, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_ChangeName_Sets_FilePath_Property()
        {
            var filePath = @"C:\SmartSession\Files\new_file.gp";
            string fileTitle = " ";

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(filePath, fileTitle);

            Assert.That(fileAttachment.SourceFilePath, Is.EqualTo(filePath));

        }

        [Test]
        public void FileAttachment_Constructor_Sets_FilePath_Property()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string fileTitle = " ";

            var fileAttachment = new FileAttachment(filePath, fileTitle);

            Assert.That(fileAttachment.SourceFilePath, Is.EqualTo(filePath));

        }

        [Test]
        public void FileAttachmentService_Add_FileAttachment_With_No_SourceFile_ThrowsException()
        {
            string fileTitle = " ";

            var fileAttachment = new FileAttachment();
            fileAttachment.ChangeName(null, fileTitle);

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
            var fileAttachment = new FileAttachment();
            
            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            
            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            fileAttachment.ChangeName(@"C:\SomeOtherFolder\Files\new_file.gp", null);
            TestDelegate testDelegate = () => fileAttachmentService.Add(fileAttachment);
            Assert.That(testDelegate, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void FileAttachmentService_Add_FileAttachment_Successfully_Invokes_Copy_Method()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileAttachment = new FileAttachment();
            var fileAttachmentsRepository = new Mock<IFileAttachmentRepository>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);
            
            fileAttachment.ChangeName(@"C:\SomeOtherFolder\Files\new_file.gp", null);
            fileAttachmentService.Add(fileAttachment);

            fileService.Verify(mock => mock.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Once, "Copy was not called.");
        }

        [Test]
        public void FileAttachmentService_Update_FileAttachment_With_New_SourceFilePath_Invokes_Copy()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileAttachment = new FileAttachment();
            fileAttachment.Id = 23;
            fileAttachment.ChangeName(@"C:\SomeOtherFolder\Files\new_file.gp", null);

            var fileAttachmentsRepository = new Mock<IFileAttachmentRepository>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            fileAttachmentService.Update(fileAttachment);
            fileService.Verify(mock => mock.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Once, "Copy was not called.");
        }

        [Test]
        public void FileAttachmentService_Update_FileAttachment_With_No_SourceFile_DoesNot_Invoke_Copy()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.FileAttachments).Returns(new Mock<IFileAttachmentRepository>().Object);

            var fileAttachment = new FileAttachment();
            fileAttachment.Id = 23;
            fileAttachment.ChangeName(null, "NewFileName.gp");

            var fileAttachmentsRepository = new Mock<IFileAttachmentRepository>();

            var fileService = new Mock<IFileService>();
            fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);
            fileService.Setup(s => s.FolderPath).Returns(@"C:\SmartSession\Files");

            var fileAttachmentService = new FileAttachmentService(unitOfWork.Object, fileService.Object);

            fileAttachmentService.Update(fileAttachment);
            fileService.Verify(mock => mock.Copy(It.IsAny<string>(), It.IsAny<string>()), Times.Never, "Copy was called, but shouldn't have.");
        }
    }
}
