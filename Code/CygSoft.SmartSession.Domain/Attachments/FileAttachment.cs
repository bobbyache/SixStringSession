using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public class FileAttachment : Entity
    {
        public FileAttachment() { }

        public FileAttachment(string filePath, string title)
        {
            this.SourceFilePath = filePath;
            Extension = Path.GetExtension(filePath);
            this.Title = string.IsNullOrWhiteSpace(title) ? Path.GetFileNameWithoutExtension(filePath) : title;
        }

        // since we can't use the primary key for part of the file name because
        // we don't know the primary key until we've saved the file... we have 
        // to make another field.
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FileId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Extension { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        [NotMapped]
        internal string SourceFilePath { get; set; }

        [NotMapped]
        public string FileName
        {
            get
            {
                if (FileId == null || Extension == null)
                    return null;
                return FileId + Extension;
            }
        }

        public void ChangeName(string filePath, string title)
        {
            this.SourceFilePath = filePath;
            Extension = Path.GetExtension(filePath);
            this.Title = string.IsNullOrWhiteSpace(title) ? Path.GetFileNameWithoutExtension(filePath) : title;
        }

        public List<FileAttachmentKeyword> FileAttachmentKeywords { get; set; }
    }
}
