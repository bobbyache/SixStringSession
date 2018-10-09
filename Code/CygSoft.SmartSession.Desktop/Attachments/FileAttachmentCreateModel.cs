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

        private string filePath;
        [ValidFilePathValidator]
        public string FilePath
        {
            get { return filePath; }
            set
            {
                Extension = Path.GetExtension(value);
                if (string.IsNullOrWhiteSpace(FileTitle))
                    FileTitle = Path.GetFileNameWithoutExtension(value);

                Set(() => FilePath, ref filePath, value, true, true);
            }
        }

        public override void Commit()
        {
            base.Commit();
            FileAttachment.ChangeName(this.FilePath, this.FileTitle);
        }
    }
}
