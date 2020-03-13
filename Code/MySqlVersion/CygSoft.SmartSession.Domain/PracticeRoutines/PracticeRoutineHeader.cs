using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineHeader
    {
        public int Id { get; }
        public string Title { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateModified { get; }

        public PracticeRoutineHeader(int id, string title, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            Title = title;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
    }
}
