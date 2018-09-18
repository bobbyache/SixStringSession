using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private IExerciseRepository repository;

        public ExerciseService(IExerciseRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException("Repository must be provided.");
        }

        public void Add(Exercise exercise)
        {
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");

            exercise.DateCreated = DateTime.Now;
            exercise.DateModified = exercise.DateCreated;

            repository.Add(exercise);
            repository.SaveChanges();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            repository.Remove(id);
            repository.SaveChanges();
        }

        public IEnumerable<Exercise> Find(string titleFragment)
        {
            var specification = new ExerciseTitleSpecification(titleFragment);
            return repository.Find(specification);
        }

        public Exercise Get(int id)
        {
            return repository.Get(id);
        }

        public void Update(Exercise exercise)
        {
            if (exercise.Id <= 0)
                throw new ArgumentException("An existing exercise must have an id");

            exercise.DateModified = DateTime.Now;

            repository.Update(exercise);
            repository.SaveChanges();
        }
    }
}
