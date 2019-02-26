using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;

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
                AssignedPracticeTime = assignedPracticeTime ?? 5000,
                Title = exercise.Title,
                PracticalityRating = practicalityRating ?? 0,
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
                SpeedProgressWeighting = speedProgressWeighting ?? 0,
                TargetMetronomeSpeed = targetSpeed ?? 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting ?? 0,
                ManualProgressWeighting = manualProgressWeighting ?? 0,
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
                SpeedProgressWeighting = speedProgressWeighting ?? 0,
                TargetMetronomeSpeed = targetSpeed ?? 0,
                PracticeTimeProgressWeighting = practiceTimeProgressWeighting ?? 0,
                ManualProgressWeighting = manualProgressWeighting ?? 0,
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
                MetronomeSpeed = speed ?? 80,
                ManualProgress = manualProgress ?? 0,
                Seconds = seconds ?? 3000,
                ExerciseId = exerciseId ?? 0,
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
                MetronomeSpeed = speed ?? 80,
                ManualProgress = manualProgress ?? 0,
                Seconds = seconds ?? 3000,
                ExerciseId = exerciseId,
                DateCreated = !string.IsNullOrEmpty(dateCreated) ? DateTime.Parse(dateCreated) : DateTime.Now,
                DateModified = null
            };
            return exerciseActivity;
        }
    }
}
