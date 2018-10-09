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

        public FileAttachment(string filePath, string fileTitle)
        {
            this.SourceFilePath = filePath;

            BuildDefinition(filePath, fileTitle);
        }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string FileTitle { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Extension { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        [NotMapped]
        internal string SourceFilePath { get; set; }

        [NotMapped]
        public string FileName => FileTitle + Extension;

        public void ChangeName(string filePath, string fileTitle)
        {
            this.SourceFilePath = filePath;

            if (filePath == null)
                this.FileTitle = fileTitle;
            else
                BuildDefinition(filePath, fileTitle);
        }

        public List<FileAttachmentKeyword> FileAttachmentKeywords { get; set; }

        private void BuildDefinition(string path, string title = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                FileTitle = Path.GetFileNameWithoutExtension(title);
                Extension = Path.GetExtension(path);
            }
            else
            {
                FileTitle = Path.GetFileNameWithoutExtension(path);
                Extension = Path.GetExtension(path);
            }
        }
    }
}
