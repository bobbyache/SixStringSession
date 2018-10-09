using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public class FileAttachmentService : IFileAttachmentService
    {
        private IUnitOfWork unitOfWork;
        private IFileService fileService;

        public FileAttachmentService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
            this.fileService = fileService ?? throw new ArgumentNullException("FileService must be provided.");
        }

        public void Add(FileAttachment fileAttachment)
        {
            if (fileAttachment.Id > 0)
                throw new ArgumentException("A new file attachment cannot have an id");

            if (fileService.FileExists(fileAttachment.FileName))
                throw new InvalidOperationException("This operation is invalid as it will overwrite an existing file with the same name.");

            if (string.IsNullOrEmpty(fileAttachment.SourceFilePath))
                throw new InvalidOperationException("Source file path has not been specified.");

            fileService.Copy(fileAttachment.SourceFilePath, Path.Combine(fileService.FolderPath, fileAttachment.FileName));

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

            fileService.Delete(fileAttachment.FileName);

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

            if (!string.IsNullOrEmpty(fileAttachment.SourceFilePath))
                fileService.Copy(fileAttachment.SourceFilePath, Path.Combine(fileService.FolderPath, fileAttachment.FileName));

            fileAttachment.DateModified = DateTime.Now;
            
            unitOfWork.FileAttachments.Update(fileAttachment);
            unitOfWork.Complete();
        }
    }
}
