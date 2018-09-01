﻿using System;
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

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

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
            if (exercise.Id <= 0)
                throw new ArgumentException("An existing exercise must have an id");

            exercise.DateModified = DateTime.Now;

            repository.Update(exercise);
            repository.SaveChanges();
        }
    }
}
