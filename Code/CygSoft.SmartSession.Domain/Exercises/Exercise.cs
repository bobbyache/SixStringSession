using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class Exercise : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        public int DifficultyRating { get; set; }
        public int PracticalityRating { get; set; }
        public int? InitialMetronomeSpeed { get; set; }
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

                if (TargetMetronomeSpeed == 0)
                    return 0;

                if (InitialMetronomeSpeed.HasValue)
                {
                    var lastComfortSpeed = GetLastActivityComfortSpeed();
                    if (lastComfortSpeed <= InitialMetronomeSpeed.Value)
                        return 0;

                    var numerator = (double)(TargetMetronomeSpeed.Value - lastComfortSpeed);
                    var denominator = (double)(TargetMetronomeSpeed.Value - InitialMetronomeSpeed.Value);
                    
                    return (numerator / denominator) * 100d;
                }
                else
                {
                    var firstComfortSpeed = GetFirstActivityComfortSpeed();
                    var lastComfortSpeed = GetLastActivityComfortSpeed();

                    if (lastComfortSpeed <= firstComfortSpeed)
                        return 0;

                    // stagger backwards
                    var numerator = (double)(TargetMetronomeSpeed.Value - firstComfortSpeed) - (lastComfortSpeed - firstComfortSpeed);
                    var denominator = (double)(TargetMetronomeSpeed.Value - firstComfortSpeed);

                    return (numerator / denominator) * 100d;
                }
            }
            else
            {
                if (!TargetPracticeTime.HasValue)
                    return 0;

                return (GetSecondsPracticed() / TargetPracticeTime.Value) * 100d;
            }
        }

        private int GetFirstActivityComfortSpeed()
        {
            // get the last record, get the comfort speed.
            var lastActivityDate = ExerciseActivity.Min(a => a.EndTime);
            var comfortSpeed = ExerciseActivity.Where(a => a.EndTime == lastActivityDate).Select(a => a.ComfortMetronomeSpeed).SingleOrDefault();
            return comfortSpeed;
        }

        private int GetLastActivityComfortSpeed()
        {
            // get the last record, get the comfort speed.
            var lastActivityDate = ExerciseActivity.Max(a => a.EndTime);
            var comfortSpeed = ExerciseActivity.Where(a => a.EndTime == lastActivityDate).Select(a => a.ComfortMetronomeSpeed).SingleOrDefault();
            return comfortSpeed;
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
