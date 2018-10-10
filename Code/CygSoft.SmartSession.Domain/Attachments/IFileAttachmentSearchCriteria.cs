using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public interface IFileAttachmentSearchCriteria
    {
        DateTime? DateCreatedAfter { get; set; }
        DateTime? DateCreatedBefore { get; set; }
        DateTime? DateModifiedAfter { get; set; }
        DateTime? DateModifiedBefore { get; set; }
        bool? HasNotes { get; set; }
        string Title { get; set; }
        string Extension { get; set; }
        string Keywords { get; set; }
    }
}
