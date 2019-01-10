using System;
using CygSoft.SmartSession.Domain.Common;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseSearchCriteria
    {
        DateTime? FromDateCreated { get; set; }
        DateTime? ToDateCreated { get; set; }
        DateTime? FromDateModified { get; set; }
        DateTime? ToDateModified { get; set; }
        string Title { get; set; }

        string Keywords { get; set; }
    }
}