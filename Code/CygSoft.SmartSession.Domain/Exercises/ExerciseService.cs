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
            repository.Add(exercise);
            repository.SaveChanges();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }

        public IEnumerable<Exercise> Find(string titleFragment)
        {
            return repository.Find(titleFragment);
        }

        public Exercise Get(int id)
        {
            return repository.Get(id);
        }

        public void Update(Exercise exercise)
        {
            repository.Update(exercise);
            repository.SaveChanges();
        }
    }
}
