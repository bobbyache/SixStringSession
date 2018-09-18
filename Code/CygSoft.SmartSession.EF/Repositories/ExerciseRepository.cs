using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class ExerciseRepository : BaseRepository, IExerciseRepository
    {
        public ExerciseRepository(SmartSessionContext context) : base(context) { }

        public void Add(Exercise exercise)
        {
            context.Exercises.Add(exercise);
        }

        public void Delete(int id)
        {
            Exercise exercise = Get(id);
            context.Exercises.Remove(exercise);
        }

        public IEnumerable<Exercise> Find(string titleFragment)
        {
            // Leverage the Specification Pattern, see:
            // https://hendryluk.wordpress.com/2009/03/23/extensible-query-with-specification-patterns/
            // and other links under Entity Framework in your PracticeApplication.codecat.
            if (!string.IsNullOrEmpty(titleFragment))
                return context.Exercises.Where(ex => ex.Title.Contains(titleFragment));
            else
                return context.Exercises;
        }

        public Exercise Get(int id)
        {
            var exercise = context.Exercises
                .Where(s => s.Id == id)
                .SingleOrDefault();
            return exercise;
        }

        public void Update(Exercise exercise)
        {
            var existingExercise = Get(exercise.Id);

            existingExercise.Title = exercise.Title;
            existingExercise.Notes = exercise.Notes;
            existingExercise.OptimalDuration = exercise.OptimalDuration;
            existingExercise.PracticalityRating = exercise.PracticalityRating;
            existingExercise.Scribed = exercise.Scribed;
            existingExercise.DifficultyRating = exercise.DifficultyRating;
            existingExercise.DateModified = exercise.DateModified;

            context.Update(existingExercise);
        }
    }
}
