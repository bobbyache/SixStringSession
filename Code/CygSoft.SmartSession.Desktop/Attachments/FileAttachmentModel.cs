using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Attachments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentModel : ValidatableObservableObject, INotifyDataErrorInfo
    {
        public FileAttachment FileAttachment { get; }

        public int Id { get; set; }

        private string extension;
        [Required]
        public string Extension
        {
            get { return extension; }
            private set
            {
                Set(() => Extension, ref extension, value, true, true);
            }
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

        private string fileTitle;
        [ValidFileName(RequireExtension = false)]
        public string FileTitle
        {
            get { return fileTitle; }
            set
            {
                Set(() => FileTitle, ref fileTitle, value, true, true);
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                Set(() => Notes, ref notes, value, true, true);
            }
        }

        public FileAttachmentModel(FileAttachment fileAttachment)
        {
            this.FileAttachment = fileAttachment;
            Revert();
            ValidateAll();
            TrackChanges = true;
        }

        public override void Commit()
        {
            Mapper.Map(this, FileAttachment);

            FileAttachment.ChangeName(this.FilePath, this.FileTitle);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(FileAttachment, this);
            base.Revert();
        }
    }
}
