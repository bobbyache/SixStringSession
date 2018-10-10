using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Attachments;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
            protected set
            {
                Set(() => Extension, ref extension, value, true, true);
            }
        }

        private string fileId;
        public string FileId
        {
            get { return fileId; }
            set
            {
                Set(() => FileId, ref fileId, value, true, true);
            }
        }

        private string title;
        [Required]
        public string Title
        {
            get { return title; }
            set
            {
                Set(() => Title, ref title, value, true, true);
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

        public string FileName { get { return FileId + Extension; } }

        public FileAttachmentModel(FileAttachment fileAttachment)
        {
            this.FileAttachment = fileAttachment ?? throw new ArgumentNullException("FileAttachment is a required parameter and must be defined.");

            Revert();
            ValidateAll();
            TrackChanges = true;
        }

        public override void Commit()
        {
            Mapper.Map(this, FileAttachment);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(FileAttachment, this);
            base.Revert();
        }
    }
}
