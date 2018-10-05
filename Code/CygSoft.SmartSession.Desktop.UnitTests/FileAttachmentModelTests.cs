using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Attachments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class FileAttachmentModelTests
    {
        [Test]
        public void FileAttachmentModel_Assigned_An_FileAttachment_In_Constructor_Is_Not_Dirty()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.FileTitle = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

            Assert.That(fileAttachmentModel.IsDirty, Is.False);
        }

        //[Test]
        //public void FileAttachmentModel_Initialized_With_New_File_Attachment_Has_ValidationErrors()
        //{
        //    var fileAttachment = new FileAttachment();
        //    var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

        //    Assert.That(fileAttachmentModel.HasErrors, Is.True);
        //}

        [Test]
        public void FileAttachmentModel_ChangeFilePath_Is_Now_Dirty()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.FileTitle = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);
            fileAttachmentModel.FilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.IsDirty, Is.True);
        }


        [Test]
        public void FileAttachmentModel_Assigned_An_FileAttachment_In_Constructor_Has_Proper_Initial_State()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.FileTitle = "current_file_title";
            fileAttachment.Extension = ".txt";
            fileAttachment.Notes = "Some notes.";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(fileAttachment.Extension));
            Assert.That(fileAttachmentModel.FileTitle, Is.EqualTo(fileAttachment.FileTitle));
            Assert.That(fileAttachmentModel.Notes, Is.EqualTo(fileAttachment.Notes));
        }

        [Test]
        public void FileAttachmentModel_ChangeFilePath_Updates_TitleAndExtension()
        {
            var fileAttachment = new FileAttachment();
            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);

            fileAttachmentModel.FilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.FileTitle, Is.EqualTo("newFile"));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void FileAttachmentModel_ChangeFilePath_With_Existing_Title_DoesNot_Change_Title()
        {
            var fileAttachment = new FileAttachment();
            fileAttachment.FileTitle = "current_file_title";
            fileAttachment.Extension = ".txt";

            var fileAttachmentModel = new FileAttachmentModel(fileAttachment);
            fileAttachmentModel.FilePath = @"C:\SmartSession\newFile.gp";

            Assert.That(fileAttachmentModel.FileTitle, Is.EqualTo("current_file_title"));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(".gp"));
        }

        [Test]
        public void TestFilenameAttribute()
        {
            var rxa = new ValidFileNameAttribute();
            Assert.IsFalse(rxa.IsValid("pptx."));
            Assert.IsFalse(rxa.IsValid("pp.tx."));
            Assert.IsFalse(rxa.IsValid("."));
            Assert.IsFalse(rxa.IsValid(".pp.tx"));
            Assert.IsFalse(rxa.IsValid(".pptx"));
            Assert.IsFalse(rxa.IsValid("pptx"));
            Assert.IsFalse(rxa.IsValid("a/abc.pptx"));
            Assert.IsFalse(rxa.IsValid("a\\abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c:abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c<abc.pptx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
            rxa = new ValidFileNameAttribute { AllowedExtensions = ".pptx" };
            Assert.IsFalse(rxa.IsValid("abc.docx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
        }


        [Test]
        public void TestFilenameAttribute_2()
        {
            var rxa = new ValidFileNameAttribute();
            rxa.RequireExtension = false;

            Assert.IsFalse(rxa.IsValid("pptx."));
            Assert.IsFalse(rxa.IsValid("pp.tx."));
            Assert.IsFalse(rxa.IsValid("."));
            Assert.IsFalse(rxa.IsValid(".pp.tx"));
            Assert.IsFalse(rxa.IsValid(".pptx"));
            Assert.IsTrue(rxa.IsValid("pptx"));
            Assert.IsFalse(rxa.IsValid("a/abc.pptx"));
            Assert.IsFalse(rxa.IsValid("a\\abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c:abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c<abc.pptx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
            rxa = new ValidFileNameAttribute { AllowedExtensions = ".pptx" };
            Assert.IsFalse(rxa.IsValid("abc.docx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
        }
    }
}
