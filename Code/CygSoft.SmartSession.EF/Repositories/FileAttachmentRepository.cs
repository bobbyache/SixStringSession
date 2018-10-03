using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class FileAttachmentRepository : BaseRepository<FileAttachment>, IFileAttachmentRepository
    {
        public FileAttachmentRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<FileAttachment> Find(Specification<FileAttachment> specification, int page = 0, int pageSize = 100)
        {
            var exs = context.FileAttachments
                .Where(specification.ToExpression())
            ;

            return exs.ToList();
        }

        public IReadOnlyList<FileAttachment> Find(Specification<FileAttachment> specification, string[] keywords, int page = 0, int pageSize = 100)
        {
            var exs = context.FileAttachments
                //.Include(ex => ex.ExerciseKeywords)
                //.ThenInclude(keyword => keyword.Keyword)
                .Where(specification.ToExpression())

                .Where(file => file.FileAttachmentKeywords
                    .Select(fileKey => fileKey.Keyword.Word)
                    //.Where(w => w == "Vibrato")
                    .Where(w => keywords.Contains(w))
                    .Any()
                    )
            ;

            return exs.ToList();
        }
    }
}
