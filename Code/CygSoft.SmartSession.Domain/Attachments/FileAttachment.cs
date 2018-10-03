using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.Attachments
{
    public class FileAttachment : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string FileTitle { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string Extension { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        public List<FileAttachmentKeyword> FileAttachmentKeywords { get; set; }
    }
}
