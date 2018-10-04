using System;
using CygSoft.SmartSession.Domain.Common;

namespace CygSoft.SmartSession.Domain.GoalTasks
{
    public interface IGoalTaskSearchCriteria
    {
        DateTime? DateCreatedAfter { get; set; }
        DateTime? DateCreatedBefore { get; set; }
        DateTime? DateModifiedAfter { get; set; }
        DateTime? DateModifiedBefore { get; set; }

        bool? HasNotes { get; set; }
        string Title { get; set; }
        string Keywords { get; set; }
    }
}