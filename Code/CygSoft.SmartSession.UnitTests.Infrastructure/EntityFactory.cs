using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.UnitTests.Infrastructure
{
    public class EntityFactory
    {
        public static ExerciseActivity CreateExerciseActivity(int? exerciseId = null, int? speed = null, int? manualProgress = null, int? seconds = null,
            string startTime = null, string endTime = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = 0, // we're creating an exercise, not gettings it.
                MetronomeSpeed = speed.HasValue ? speed.Value : 80,
                ManualProgress = manualProgress.HasValue ? manualProgress.Value : 0,
                Seconds = seconds.HasValue ? seconds.Value : 3000,
                StartTime = !string.IsNullOrEmpty(startTime) ? DateTime.Parse(startTime) : DateTime.Parse("2018-03-01 12:15:00"),
                EndTime = !string.IsNullOrEmpty(endTime) ? DateTime.Parse(endTime) : DateTime.Parse("2018-03-01 12:25:00"),
                ExerciseId = exerciseId.HasValue ? exerciseId.Value : 0
            };
            return exerciseActivity;
        }

        public static ExerciseActivity GetExerciseActivity(int id, int exerciseId, int? speed = null, int? manualProgress = null, int? seconds = null,
            string startTime = null, string endTime = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = id, // we're getting an existing exercise.
                MetronomeSpeed = speed.HasValue ? speed.Value : 80,
                ManualProgress = manualProgress.HasValue ? manualProgress.Value : 0,
                Seconds = seconds.HasValue ? seconds.Value : 3000,
                StartTime = !string.IsNullOrEmpty(startTime) ? DateTime.Parse(startTime) : DateTime.Parse("2018-03-01 12:15:00"),
                EndTime = !string.IsNullOrEmpty(endTime) ? DateTime.Parse(endTime) : DateTime.Parse("2018-03-01 12:25:00"),
                ExerciseId = exerciseId
            };
            return exerciseActivity;
        }
    }
}
