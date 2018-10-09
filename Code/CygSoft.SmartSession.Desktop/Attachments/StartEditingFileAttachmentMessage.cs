using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public enum StartEditingEntityMode
    {
        Create,
        Update
    }

    internal class StartEditingFileAttachmentMessage
    {
        public StartEditingEntityMode Mode { get; }
        public FileAttachmentSearchResultModel FileAttachmentSearchResult { get; }

        public StartEditingFileAttachmentMessage(FileAttachmentSearchResultModel fileAttachmentSearchResult, StartEditingEntityMode mode)
        {
            Mode = mode;
            FileAttachmentSearchResult = fileAttachmentSearchResult;
        }
    }
}
