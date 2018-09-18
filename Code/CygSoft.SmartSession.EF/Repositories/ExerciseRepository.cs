using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            return context.Exercises.Where(ex => specification.IsSatisfiedBy(ex)).ToList();
        }

        public override Exercise Get(int id)
        {
            var exercise = context.Exercises
                .Where(s => s.Id == id)
                .SingleOrDefault();
            return exercise;
        }

        public override void Add(Exercise exercise)
        {
            context.Exercises.Add(exercise);
        }

        public override void Remove(Exercise entity)
        {
            Exercise exercise = Get(entity.Id);
            context.Exercises.Remove(exercise);
        }

        public override void Remove(int id)
        {
            Exercise exercise = Get(id);
            context.Exercises.Remove(exercise);
        }

        public override void Update(Exercise entity)
        {
            var existingExercise = Get(entity.Id);

            existingExercise.Title = entity.Title;
            existingExercise.Notes = entity.Notes;
            existingExercise.OptimalDuration = entity.OptimalDuration;
            existingExercise.PracticalityRating = entity.PracticalityRating;
            existingExercise.Scribed = entity.Scribed;
            existingExercise.DifficultyRating = entity.DifficultyRating;
            existingExercise.DateModified = entity.DateModified;

            context.Update(existingExercise);
        }
    }
}
