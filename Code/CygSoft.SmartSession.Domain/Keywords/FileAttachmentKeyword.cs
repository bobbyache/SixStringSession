using CygSoft.SmartSession.Domain.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Keywords
{
    public class FileAttachmentKeyword
    {
        public int FileAttachmentId { get; set; }
        public FileAttachment Attachment { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
