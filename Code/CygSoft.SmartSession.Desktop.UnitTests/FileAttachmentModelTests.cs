using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Domain.Attachments;
using NUnit.Framework;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class FileAttachmentModelTests
    {
        [Test]
        public void FileAttachmentModel_Assigned_An_FileAttachment_In_Constructor_Is_Not_Dirty()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.Title = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

            Assert.That(fileAttachmentModel.IsDirty, Is.False);
        }

        [Test]
        public void FileAttachmentModel_ChangeFilePath_Is_Now_Dirty()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.Title = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentCreateModel(fileAttachment);
            fileAttachmentModel.SourceFilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.IsDirty, Is.True);
        }


        [Test]
        public void FileAttachmentModel_Assigned_An_FileAttachment_In_Constructor_Has_Proper_Initial_State()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.Title = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(fileAttachment.Extension));
            Assert.That(fileAttachmentModel.Title, Is.EqualTo(fileAttachment.Title));
            Assert.That(fileAttachmentModel.Notes, Is.EqualTo(fileAttachment.Notes));
        }

        [Test]
        public void FileAttachmentModel_ChangeFilePath_Updates_TitleAndExtension()
        {
            var fileAttachment = new FileAttachment();
            var fileAttachmentModel = new FileAttachmentCreateModel(fileAttachment);

            fileAttachmentModel.SourceFilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.Title, Is.EqualTo("newFile"));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachmentModel_ChangeFilePath_With_Existing_Title_DoesNot_Change_Title()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.Title = "current_file_title";
            fileAttachment.Extension = ".txt";

            var fileAttachmentModel = new FileAttachmentCreateModel(fileAttachment);
            fileAttachmentModel.SourceFilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.Title, Is.EqualTo("current_file_title"));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(".gp"));
        }
    }
}
