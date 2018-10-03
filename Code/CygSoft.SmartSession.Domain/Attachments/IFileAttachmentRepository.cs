using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public interface IFileAttachmentRepository : IRepository<FileAttachment>
    {
        IReadOnlyList<FileAttachment> Find(Specification<FileAttachment> specification, string[] keywords, int page = 0, int pageSize = 100);
    }
}
