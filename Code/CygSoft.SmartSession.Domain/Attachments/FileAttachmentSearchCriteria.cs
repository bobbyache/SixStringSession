using CygSoft.SmartSession.Domain.Attachments.Specifications;
using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public class FileAttachmentSearchCriteria : IFileAttachmentSearchCriteria
    {
        public DateTime? DateCreatedAfter { get; set; }
        public DateTime? DateCreatedBefore { get; set; }
        public DateTime? DateModifiedAfter { get; set; }
        public DateTime? DateModifiedBefore { get; set; }
        public bool? HasNotes { get; set; }
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Keywords { get; set; }

        public string[] KeywordSpecification()
        {
            return Keywords.Split(new char[] { ',' });
        }

        public Specification<FileAttachment> Specification()
        {
            return
                new FileAttachmentTitleSpecification(Title)
                .And(new FileAttachmentDateModifiedSpecification(DateModifiedAfter, DateModifiedBefore))
                .And(new FileAttachmentDateCreatedSpecification(DateCreatedAfter, DateCreatedBefore))
                .And(new FileAttachmentHasNotesSpecification(HasNotes))
                .And(new FileAttachmentExtensionSpecification(Extension))
            ;
        }
    }
}
