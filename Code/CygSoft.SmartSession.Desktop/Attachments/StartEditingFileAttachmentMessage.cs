using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    internal class StartEditingFileAttachmentMessage
    {
        public FileAttachmentSearchResult FileAttachmentSearchResult { get; }

        public StartEditingFileAttachmentMessage(FileAttachmentSearchResult fileAttachmentSearchResult)
        {
            FileAttachmentSearchResult = fileAttachmentSearchResult;
        }
    }
}
