using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private IUnitOfWork unitOfWork;

        public ExerciseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
        }

        public void Add(Exercise exercise)
        {
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");

            exercise.DateCreated = DateTime.Now;
            exercise.DateModified = exercise.DateCreated;

            unitOfWork.Exercises.Add(exercise);
            unitOfWork.Complete();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var exercise = unitOfWork.Exercises.Get(id);
            unitOfWork.Exercises.Remove(exercise);
            unitOfWork.Complete();
        }

        public IEnumerable<Exercise> Find(string titleFragment)
        {
            var specification = new ExerciseTitleSpecification(titleFragment);
            return unitOfWork.Exercises.Find(specification);
        }

        public Exercise Get(int id)
        {
            return unitOfWork.Exercises.Get(id);
        }

        public void Update(Exercise exercise)
        {
            if (exercise.Id <= 0)
                throw new ArgumentException("An existing exercise must have an id");

            exercise.DateModified = DateTime.Now;

            unitOfWork.Exercises.Update(exercise);
            unitOfWork.Complete();
        }
    }
}
