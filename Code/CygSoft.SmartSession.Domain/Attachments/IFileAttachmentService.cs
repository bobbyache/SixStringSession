using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public interface IFileAttachmentService
    {
        FileAttachment Get(int id);
        IEnumerable<FileAttachment> Find(FileAttachmentSearchCriteria searchCriteria);
        void Remove(int id);
        void Add(FileAttachment fileAttachment);
        void Update(FileAttachment fileAttachment);
    }
}
