using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Recording;
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

        public void Add(IExercise exercise)
        {
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");

            exercise.DateCreated = DateTime.Now;
            exercise.DateModified = exercise.DateCreated;

            unitOfWork.Exercises.Add(exercise);
            unitOfWork.Commit();
        }

        public IExercise Create()
        {
            var exercise = new Exercise()
            {
                Title = $"New Exercise Item - {DateTime.Now}",
            };
            return exercise;
        }

        public ExerciseActivity CreateExerciseActivity(int speed, int seconds, int manualProgress)
        {
            var exerciseActivity = new ExerciseActivity
            {
                MetronomeSpeed = speed,
                ManualProgress = manualProgress,
                Seconds = seconds
            };
            return exerciseActivity;
        }

        public void Remove(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("The Id is invalid and must be greater than 0.");

                var exercise = unitOfWork.Exercises.Get(id);
                unitOfWork.Exercises.Remove(exercise);
                unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                //TODO: AOP Logging of Errors! log error...
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public IEnumerable<IExercise> Find(ExerciseSearchCriteria searchCriteria)
        {
            return unitOfWork.Exercises.Find(searchCriteria);
        }

        public IEnumerable<IExercise> GetPracticeRoutineExercises(int practiceRoutineId)
        {
            return unitOfWork.Exercises.GetPracticeRoutineExercises(practiceRoutineId);
        }

        public IExercise Get(int id)
        {
            return unitOfWork.Exercises.Get(id);
        }

        public void Update(IExercise exercise)
        {
            if (exercise.Id <= 0)
                throw new ArgumentException("An existing exercise must have an id");

            unitOfWork.Exercises.Update(exercise);
            unitOfWork.Commit();
        }

        public void Update(IEnumerable<IExercise> exercises)
        {
            foreach (var exercise in exercises)
            {
                if (exercise.Id <= 0)
                    throw new ArgumentException("An existing exercise must have an id");
            }

            unitOfWork.Exercises.Update(exercises);
            unitOfWork.Commit();
        }

        public IExerciseRecorder GetExerciseRecorder(int exerciseId)
        {
            return unitOfWork.Exercises.GetExerciseRecorder(exerciseId);
        }
    }
}
