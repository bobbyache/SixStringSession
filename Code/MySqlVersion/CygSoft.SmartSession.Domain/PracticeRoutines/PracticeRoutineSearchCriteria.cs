using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineSearchCriteria : IPracticeRoutineSearchCriteria
    {
        public DateTime? FromDateCreated { get; set; }
        public DateTime? ToDateCreated { get; set; }
        public DateTime? FromDateModified { get; set; }
        public DateTime? ToDateModified { get; set; }
        public string Title { get; set; }
    }
}
