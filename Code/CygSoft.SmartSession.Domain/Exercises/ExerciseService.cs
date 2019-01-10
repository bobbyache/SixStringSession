using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private IUnitOfWork unitOfWork;
        private IFileService fileService;

        public ExerciseService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
            this.fileService = fileService ?? throw new ArgumentNullException("FileService must be provided.");
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

        public ExerciseActivity CreateExerciseActivity(int speed, int seconds, int manualProgress, DateTime startTime, DateTime endTime)
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
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var exercise = unitOfWork.Exercises.Get(id);
            unitOfWork.Exercises.Remove(exercise);
            unitOfWork.Commit();
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

        public void AddFiles(int exerciseId, string[] filePaths)
        {
            fileService.AddExerciseFiles(exerciseId, filePaths);
        }

        public void DeleteFiles(int exerciseId, string[] fileNames)
        {
            fileService.DeleteExerciseFiles(exerciseId, fileNames);
        }

        public void DeleteFiles(int exerciseId)
        {
            fileService.DeleteExerciseFiles(exerciseId);
        }

        public string[] GetFiles(int exerciseId)
        {
            return fileService.GetExerciseFiles(exerciseId);
        }

        public void OpenFile(int exerciseId, string fileName)
        {
            fileService.OpenExerciseFile(exerciseId, fileName);
        }
    }
}
