using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public class FileAttachmentService : IFileAttachmentService
    {
        private IUnitOfWork unitOfWork;

        public FileAttachmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
        }

        public void Add(FileAttachment fileAttachment)
        {
            if (fileAttachment.Id > 0)
                throw new ArgumentException("A new file attachment cannot have an id");

            fileAttachment.DateCreated = DateTime.Now;
            fileAttachment.DateModified = fileAttachment.DateCreated;

            unitOfWork.FileAttachments.Add(fileAttachment);
            unitOfWork.Complete();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var fileAttachment = unitOfWork.FileAttachments.Get(id);
            unitOfWork.FileAttachments.Remove(fileAttachment);
            unitOfWork.Complete();
        }

        public IEnumerable<FileAttachment> Find(FileAttachmentSearchCriteria searchCriteria)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria.Keywords))
                return unitOfWork.FileAttachments.Find(searchCriteria.Specification());

            else
                return unitOfWork.FileAttachments.Find(searchCriteria.Specification(), searchCriteria.KeywordSpecification());
        }

        public FileAttachment Get(int id)
        {
            return unitOfWork.FileAttachments.Get(id);
        }

        public void Update(FileAttachment fileAttachment)
        {
            if (fileAttachment.Id <= 0)
                throw new ArgumentException("An existing file attachment must have an id");

            fileAttachment.DateModified = DateTime.Now;

            unitOfWork.FileAttachments.Update(fileAttachment);
            unitOfWork.Complete();
        }
    }
}
