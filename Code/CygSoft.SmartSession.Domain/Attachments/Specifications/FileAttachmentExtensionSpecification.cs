using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments.Specifications
{
    public class FileAttachmentExtensionSpecification : Specification<FileAttachment>
    {
        private readonly string extension;

        public FileAttachmentExtensionSpecification(string extension)
        {
            this.extension = extension;
        }

        public override Expression<Func<FileAttachment, bool>> ToExpression()
        {
            if (string.IsNullOrEmpty(extension))
                return ex => true;
            else
                return ex => ex.Title.ToUpper().Contains(extension.ToUpper());
        }

    }
}
