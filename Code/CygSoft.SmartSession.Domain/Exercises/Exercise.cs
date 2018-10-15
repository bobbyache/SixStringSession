using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public enum PercentCompleteCalculationStrategy
    {
        MetronomeSpeed = 0,
        PracticeTime = 1,
    }

    public class Exercise : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        public int DifficultyRating { get; set; }
        public int OptimalDuration { get; set; }
        public int PracticalityRating { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        public int? TargetMetronomeSpeed { get; set; }
        public int? TargetPracticeTime { get; set; }

        public int Weighting { get; set; } = 1000; // ratio of 1000

        public PercentCompleteCalculationStrategy PercentageCompleteCalculationType { get; set; }

        public List<ExerciseKeyword> ExerciseKeywords { get; set; }

        public List<SessionExerciseActivity> ExerciseActivity { get; set; }

        public decimal GetPercentComplete()
        {
            // https://stackoverflow.com/questions/47386256/entity-framework-calculate-sum-field-from-child-records

            //if (PercentageCompleteCalculationType == PercentCompleteCalculationStrategy.MetronomeSpeed)
            //    return 40;
            //else if (PercentageCompleteCalculationType == PercentCompleteCalculationStrategy.PracticeTime)
            //    return 60;
            //return 0;
            throw new NotImplementedException();
        }

        public int GetMinutesPracticed()
        {
            // the way you'd do this is by loading the list of related "SessionExerciseResult" objects that are listed for this
            // exercise and summ the minutes practiced.
            throw new NotImplementedException();
        }
    }
}
