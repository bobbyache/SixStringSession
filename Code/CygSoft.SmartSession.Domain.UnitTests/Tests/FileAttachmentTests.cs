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
    }
}
