using CygSoft.SmartSession.Desktop.Attachments;
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
    public class FileAttachmentSearchResultTests
    {
        [Test]
        public void FileAttachmentSearchResult_FileName_Returns_Title_And_Extension()
        {
            var fileAttachment = new FileAttachment(@"C:\SmartSession\files\file.txt", null);
            fileAttachment.Id = 8;
            var result = new FileAttachmentSearchResult();

            result.Id = fileAttachment.Id;
            result.FileTitle = fileAttachment.FileTitle;
            result.Extension = fileAttachment.Extension;

            Assert.That(result.FileName, Is.EqualTo("file.txt"));
        }
    }
}
