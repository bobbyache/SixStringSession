using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public interface IPracticeRoutineSearchCriteria
    {
        DateTime? FromDateCreated { get; set; }
        DateTime? ToDateCreated { get; set; }
        DateTime? FromDateModified { get; set; }
        DateTime? ToDateModified { get; set; }
        string Title { get; set; }
    }
}
