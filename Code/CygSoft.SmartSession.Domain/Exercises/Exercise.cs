using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure.Enums;
using System;
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

        public List<ExerciseActivity> ExerciseActivity { get; set; } = new List<ExerciseActivity>();

        public int GetLastRecordedSpeed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            var endDate = ExerciseActivity.Max(a => a.EndTime);
            return ExerciseActivity.Where(a => a.EndTime == endDate).Select(a => a.MetronomeSpeed).SingleOrDefault();
        }

        public double GetPercentComplete()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            if (PercentageCompleteCalculationType == PercentCompleteCalculationStrategy.MetronomeSpeed)
            {
                if (!TargetMetronomeSpeed.HasValue)
                    return 0;

                if (TargetMetronomeSpeed == 0)
                    return 0;

                if (InitialMetronomeSpeed.HasValue)
                {
                    var lastComfortSpeed = GetLastRecordedSpeed();
                    if (lastComfortSpeed <= InitialMetronomeSpeed.Value)
                        return 0;

                    var numerator = (double)(TargetMetronomeSpeed.Value - lastComfortSpeed);
                    var denominator = (double)(TargetMetronomeSpeed.Value - InitialMetronomeSpeed.Value);
                    
                    return (numerator / denominator) * 100d;
                }
                else
                {
                    var firstComfortSpeed = GetFirstRecordedSpeed();
                    var lastComfortSpeed = GetLastRecordedSpeed();

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

        private int GetFirstRecordedSpeed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            // get the last record, get the comfort speed.
            var lastActivityDate = ExerciseActivity.Min(a => a.EndTime);
            var comfortSpeed = ExerciseActivity.Where(a => a.EndTime == lastActivityDate).Select(a => a.MetronomeSpeed).SingleOrDefault();
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

        public ExerciseActivity AddRecording(int speed, int seconds, DateTime startTime, DateTime endTime)
        {
            var exerciseActivity = new ExerciseActivity
            {
                MetronomeSpeed = speed,
                Seconds = seconds,
                StartTime = startTime,
                EndTime = endTime,
                ExerciseId = this.Id
            };
            this.ExerciseActivity.Add(exerciseActivity);

            return exerciseActivity;
        }

        public void RemoveRecording(int exerciseActivityId)
        {
            var exerciseActivity = ExerciseActivity.Where(a => a.Id == exerciseActivityId).SingleOrDefault();
            if (exerciseActivity != null)
            {
                RemoveRecording(exerciseActivity);
            }
        }

        public void RemoveRecording(ExerciseActivity exerciseActivity)
        {
            ExerciseActivity.Remove(exerciseActivity);
        }

    }
}
