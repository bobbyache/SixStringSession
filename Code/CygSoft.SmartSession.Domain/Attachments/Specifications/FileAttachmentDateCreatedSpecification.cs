using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Attachments.Specifications
{
    public class FileAttachmentDateCreatedSpecification : Specification<FileAttachment>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public FileAttachmentDateCreatedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<FileAttachment, bool>> ToExpression()
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate && ex.DateCreated <= endDate;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                return ex => true;

            }
            else if (startDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate;
            }
            else if (endDate.HasValue)
            {
                return ex => ex.DateCreated <= endDate;
            }
            else
            {
                return ex => false;
            }
        }
    }
}
