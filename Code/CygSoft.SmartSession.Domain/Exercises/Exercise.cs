using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
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
        private class WeightedMetric : IWeightedEntity
        {
            private readonly double percentCompleted;

            public WeightedMetric(double percentComplete, int weighting)
            {
                percentCompleted = percentComplete;
                Weighting = weighting;
            }
            public int Weighting { get; private set; }

            public double PercentCompleted()
            {
                return percentCompleted;
            }
        }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        public int DifficultyRating { get; set; }
        public int PracticalityRating { get; set; }
        public int? TargetMetronomeSpeed { get; set; }
        public int? TargetPracticeTime { get; set; }

        public int SpeedProgressWeighting { get; set; } = 50; // half way...
        public int PracticeTimeProgressWeighting { get; set; } = 50; // half way...
        public int ManualProgressWeighting { get; set; } = 0; // default to no weighting.

        public List<ExerciseKeyword> ExerciseKeywords { get; set; }

        public List<ExerciseActivity> ExerciseActivity { get; set; } = new List<ExerciseActivity>();

        public int GetLastRecordedSpeed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            var endDate = ExerciseActivity.Max(a => a.EndTime);
            return ExerciseActivity.Where(a => a.EndTime == endDate).Select(a => a.MetronomeSpeed).SingleOrDefault();
        }

        public int GetLastRecordedManualProgress()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            var endDate = ExerciseActivity.Max(a => a.EndTime);
            return ExerciseActivity.Where(a => a.EndTime == endDate).Select(a => a.ManualProgress).SingleOrDefault();
        }

        public double GetPercentComplete()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            var calculator = new WeightedProgressCalculator();
            calculator.Add(new WeightedMetric(GetManualProgressPercentComplete(), ManualProgressWeighting));
            calculator.Add(new WeightedMetric(CalculateSpeedPercentComplete(), SpeedProgressWeighting));
            calculator.Add(new WeightedMetric(CalculatePracticeTimePercentComplete(), PracticeTimeProgressWeighting));

            return calculator.CalculateTotalProgress();
        }

        private double GetManualProgressPercentComplete()
        {
            return (double)GetLastManualProgressPercentage();
        }

        private double CalculatePracticeTimePercentComplete()
        {
            if (!TargetPracticeTime.HasValue)
                return 0;

            var percentComplete = (GetSecondsPracticed() / TargetPracticeTime.Value) * 100d;
            return percentComplete > 100 ? 100 : percentComplete;
        }

        private double CalculateSpeedPercentComplete()
        {
            if (!TargetMetronomeSpeed.HasValue)
                return 0;

            if (TargetMetronomeSpeed == 0)
                return 0;

            var firstSpeed = GetFirstRecordedSpeed();
            var lastSpeed = GetLastRecordedSpeed();

            if (lastSpeed <= firstSpeed)
                return 0;

            // stagger backwards
            var numerator = (double)(lastSpeed - firstSpeed);
            var denominator = (double)(TargetMetronomeSpeed.Value - firstSpeed);

            var percentComplete = (numerator / denominator) * 100d;
            return percentComplete > 100 ? 100 : percentComplete;
        }

        private int GetLastManualProgressPercentage()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            // get the last record, get the speed.
            var lastActivityDate = ExerciseActivity.Max(a => a.EndTime);
            var manualProgress = ExerciseActivity.Where(a => a.EndTime == lastActivityDate).Select(a => a.ManualProgress).SingleOrDefault();
            return manualProgress;
        }

        private int GetFirstRecordedSpeed()
        {
            if (ExerciseActivity == null || !ExerciseActivity.Any())
                return 0;

            // get the last record, get the speed.
            var lastActivityDate = ExerciseActivity.Min(a => a.EndTime);
            var speed = ExerciseActivity.Where(a => a.EndTime == lastActivityDate).Select(a => a.MetronomeSpeed).SingleOrDefault();
            return speed;
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

        public ExerciseActivity AddRecording(int speed, int seconds, int manualProgress, DateTime startTime, DateTime endTime)
        {
            var exerciseActivity = new ExerciseActivity
            {
                MetronomeSpeed = speed,
                ManualProgress = manualProgress,
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
