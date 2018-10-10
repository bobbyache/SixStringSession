using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Attachments;
using System.IO;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentCreateModel : FileAttachmentModel
    {
        public FileAttachmentCreateModel(FileAttachment fileAttachment) : base(fileAttachment)
        {
        }

        private string sourceFilePath;
        [ValidFilePathValidator]
        public string SourceFilePath
        {
            get { return sourceFilePath; }
            set
            {
                Extension = Path.GetExtension(value);
                if (string.IsNullOrWhiteSpace(Title))
                    Title = Path.GetFileNameWithoutExtension(value);

                Set(() => SourceFilePath, ref sourceFilePath, value, true, true);
            }
        }

        public override void Commit()
        {
            FileAttachment.SourceFilePath = this.SourceFilePath;
            base.Commit();
        }
    }
}
