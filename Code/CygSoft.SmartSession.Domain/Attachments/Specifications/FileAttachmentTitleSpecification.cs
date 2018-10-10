using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Attachments.Specifications
{
    public class FileAttachmentTitleSpecification : Specification<FileAttachment>
    {
        private readonly string titleFragment;

        public FileAttachmentTitleSpecification(string titleFragment)
        {
            this.titleFragment = titleFragment;
        }

        public override Expression<Func<FileAttachment, bool>> ToExpression()
        {
            if (string.IsNullOrEmpty(titleFragment))
                return ex => true;
            else
                return ex => ex.Title.ToUpper().Contains(titleFragment.ToUpper());
        }
    }
}
