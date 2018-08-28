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
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");
            context.Exercises.Add(exercise);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            Exercise exercise = Get(id);
            context.Exercises.Remove(exercise);
        }

        public IEnumerable<Exercise> Find(string titleFragment)
        {
            throw new NotImplementedException();
        }

        public Exercise Get(int id)
        {
            var exercise = context.Exercises
                .Where(s => s.Id == id)
                .SingleOrDefault();
            return exercise;
        }
    }
}
