using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseSearchCriteria : IExerciseSearchCriteria
    {
        public DateTime? FromDateCreated { get; set; }
        public DateTime? ToDateCreated { get; set; }
        public DateTime? FromDateModified { get; set; }
        public DateTime? ToDateModified { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
    }
}
