using CygSoft.SmartSession.Domain.Attachments;
using NUnit.Framework;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class FileAttachmentTests
    {
        [Test]
        public void FileAttachment_Constructor_With_ModifiedTitle_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var title = "new_file_modified_name";

            var fileAttachment = new FileAttachment(filePath, title);

            Assert.That(fileAttachment.Title, Is.EqualTo("new_file_modified_name"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_ModifiedTitle_With_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            var title = "new_file_modified_name.gp";

            var fileAttachment = new FileAttachment(filePath, title);

            Assert.That(fileAttachment.Title, Is.EqualTo("new_file_modified_name.gp"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_Null_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string title = null;

            var fileAttachment = new FileAttachment(filePath, title);

            Assert.That(fileAttachment.Title, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_With_Empty_Title_No_Extension_Has_Expected_State()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string title = " ";

            var fileAttachment = new FileAttachment(filePath, title);

            Assert.That(fileAttachment.Title, Is.EqualTo("new_file"));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachment_Constructor_Sets_FilePath_Property()
        {
            var filePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            string title = " ";

            var fileAttachment = new FileAttachment(filePath, title);

            Assert.That(fileAttachment.SourceFilePath, Is.EqualTo(filePath));
        }
    }
}
