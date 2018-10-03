using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments.Specifications
{
    public class FileAttachmentHasNotesSpecification : Specification<FileAttachment>
    {
        private readonly bool? hasNotes;

        public FileAttachmentHasNotesSpecification(bool? isScribed)
        {
            this.hasNotes = isScribed;
        }

        public override Expression<Func<FileAttachment, bool>> ToExpression()
        {
            if (hasNotes == null)
                return ex => true;

            else
                return ex => string.IsNullOrWhiteSpace(ex.Notes) == !hasNotes;
        }
    }
}
