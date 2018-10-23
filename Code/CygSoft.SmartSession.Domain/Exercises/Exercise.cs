using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CygSoft.SmartSession.Infrastructure.Enums;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class Exercise : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        public int DifficultyRating { get; set; }
        public int PracticalityRating { get; set; }
        public int? TargetMetronomeSpeed { get; set; }
        public int? TargetPracticeTime { get; set; }

        public PercentCompleteCalculationStrategy PercentageCompleteCalculationType { get; set; }

        public List<ExerciseKeyword> ExerciseKeywords { get; set; }

        public List<SessionExerciseActivity> ExerciseActivity { get; set; }

        public int GetCurrentComfortSpeed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            var endDate = ExerciseActivity.Max(a => a.EndTime);
            return ExerciseActivity.Where(a => a.EndTime == endDate).Select(a => a.ComfortMetronomeSpeed).SingleOrDefault();
        }

        public double GetPercentComplete()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            // https://stackoverflow.com/questions/47386256/entity-framework-calculate-sum-field-from-child-records

            if (PercentageCompleteCalculationType == PercentCompleteCalculationStrategy.MetronomeSpeed)
            {
                if (!TargetMetronomeSpeed.HasValue)
                    return 0;



                var startDate = ExerciseActivity.Min(a => a.StartTime);
                var startSpeed = ExerciseActivity.Where(a => a.StartTime == startDate).Select(a => a.ComfortMetronomeSpeed).SingleOrDefault();

                var endDate = ExerciseActivity.Max(a => a.EndTime);
                var endSpeed = ExerciseActivity.Where(a => a.EndTime == endDate).Select(a => a.ComfortMetronomeSpeed).SingleOrDefault();


                double startVal = TargetMetronomeSpeed.Value - startSpeed;
                double endVal = TargetMetronomeSpeed.Value - endSpeed;

                var result = (endVal / startVal) * 100d;

                return result;
            }
            else
            {
                if (!TargetPracticeTime.HasValue)
                    return 0;

                return (GetSecondsPracticed() / TargetPracticeTime.Value) * 100d;
            }

            throw new NotImplementedException();
        }

        public double GetSecondsPracticed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            // the way you'd do this is by loading the list of related "SessionExerciseResult" objects that are listed for this
            // exercise and summ the minutes practiced.
            var secPracticed = ExerciseActivity.Sum(a => a.Seconds);
            return secPracticed;

        }
    }
}
