using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
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
        public static PracticeRoutine CreatePracticeRoutine(string title)
        {
            return new PracticeRoutine()
            {
                Title = title
            };
        }

        public static PracticeRoutineExercise CreatePracticeRoutineExercise(Exercise exercise, 
            int? assignedPracticeTime = null, int? practicalityRating = 1,
                int? difficultyRating = 1)
        {
            var routineExercise = new PracticeRoutineExercise
            {
                AssignedPracticeTime = assignedPracticeTime.HasValue ? assignedPracticeTime.Value : 5000,
                Title = exercise.Title,
                PracticalityRating = practicalityRating.HasValue ? practicalityRating.Value : 0,
                DifficultyRating = difficultyRating.HasValue ? difficultyRating.Value : 0,
                ExerciseId = exercise.Id
            };
            return routineExercise;
        }

        public static Exercise GetExerciseWithNoActivity(int id, string title,
            int? targetSpeed = null,int? speedProgressWeighting = null,
            int? targetpracticeTime = null, int? practiceTimeProgressWeighting = null,
            int? manualProgressWeighting = null,
            int? practicalityRating = null, int? difficultyRating = null,
            List<ExerciseActivity> exerciseActivity = null,
            string dateCreated = null, string dateModified = null)
        {
            var exercise = new Exercise
            {
                Id = 1,
                Title = title,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Parse("2018-03-01 12:15:00"),
                DateModified = !string.IsNullOrEmpty(dateModified) ? DateTime.Parse(dateModified) : DateTime.Parse("2018-03-01 12:15:00"),
                SpeedProgressWeighting = speedProgressWeighting.HasValue ? speedProgressWeighting.Value : 0,
                TargetMetronomeSpeed = targetSpeed.HasValue ? targetSpeed.Value : 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting.HasValue ? practiceTimeProgressWeighting.Value : 0,
                ManualProgressWeighting = manualProgressWeighting.HasValue ? manualProgressWeighting.Value : 0,
                TargetPracticeTime = targetpracticeTime,
                ExerciseActivity = exerciseActivity ?? new List<ExerciseActivity>()
            };
            return exercise;
        }

        public static Exercise CreateExercise(string title,
            int? targetSpeed = null, int? speedProgressWeighting = null,
            int? targetpracticeTime = null, int? practiceTimeProgressWeighting = null,
            int? manualProgressWeighting = null,
            int? practicalityRating = null, int? difficultyRating = null,
            List<ExerciseActivity> exerciseActivity = null,
            string dateCreated = null, string dateModified = null)
        {
            var exercise = new Exercise
            {
                Title = title,
                SpeedProgressWeighting = speedProgressWeighting.HasValue ? speedProgressWeighting.Value : 0,
                TargetMetronomeSpeed = targetSpeed.HasValue ? targetSpeed.Value : 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting.HasValue ? practiceTimeProgressWeighting.Value : 0,
                ManualProgressWeighting = manualProgressWeighting.HasValue ? manualProgressWeighting.Value : 0,
                TargetPracticeTime = targetpracticeTime,
                ExerciseActivity = exerciseActivity ?? new List<ExerciseActivity>()
            };
            return exercise;
        }

        public static ExerciseActivity CreateExerciseActivity(int? exerciseId = null, int? speed = null, int? manualProgress = null, int? seconds = null, string dateCreated = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = 0, // we're creating an exercise, not gettings it.
                MetronomeSpeed = speed.HasValue ? speed.Value : 80,
                ManualProgress = manualProgress.HasValue ? manualProgress.Value : 0,
                Seconds = seconds.HasValue ? seconds.Value : 3000,
                ExerciseId = exerciseId.HasValue ? exerciseId.Value : 0,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Now,
                DateModified = null
            };
            return exerciseActivity;
        }

        public static ExerciseActivity GetExerciseActivity(int id, int exerciseId, int? speed = null, int? manualProgress = null, int? seconds = null, string dateCreated = null)
        {
            var exerciseActivity = new ExerciseActivity
            {
                Id = id, // we're getting an existing exercise.
                MetronomeSpeed = speed.HasValue ? speed.Value : 80,
                ManualProgress = manualProgress.HasValue ? manualProgress.Value : 0,
                Seconds = seconds.HasValue ? seconds.Value : 3000,
                ExerciseId = exerciseId,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Now,
                DateModified = null
            };
            return exerciseActivity;
        }
    }
}
