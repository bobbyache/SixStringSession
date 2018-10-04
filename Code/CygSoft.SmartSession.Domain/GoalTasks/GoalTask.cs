using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.GoalTasks
{ 
    // https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-1-table-per-hierarchy-tph
    // https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
    // https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines
    public class GoalTask : Entity, IWeightedEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        public int Weighting { get; set; }

        public int? MinutesPracticed { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual double PercentCompleted()
        {
            throw new NotImplementedException();
        }

        public List<GoalTaskKeyword> GoalTaskKeywords { get; set; }
        public List<PracticeSessionResult> PracticeSessionResults { get; set; }

    }
}
