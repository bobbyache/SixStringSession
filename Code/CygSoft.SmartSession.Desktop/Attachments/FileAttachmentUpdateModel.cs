using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Attachments;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentUpdateModel : FileAttachmentModel
    {
        public FileAttachmentUpdateModel(FileAttachment fileAttachment) : base(fileAttachment)
        {
        }
    }
}
