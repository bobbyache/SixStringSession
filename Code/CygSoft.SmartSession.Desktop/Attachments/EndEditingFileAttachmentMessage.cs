using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    internal class EndEditingFileAttachmentMessage
    {
        public FileAttachmentModel FileAttachmentModel { get; }

        public EndEditingFileAttachmentMessage(FileAttachmentModel fileAttachmentModel)
        {
            FileAttachmentModel = fileAttachmentModel;
        }
    }
}
