using CygSoft.SmartSession.Domain.Common;
using System;
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

        public void Add(Exercise exercise)
        {
            if (exercise.Id > 0)
                throw new ArgumentException("A new exercise cannot have an id");

            exercise.DateCreated = DateTime.Now;
            exercise.DateModified = exercise.DateCreated;

            unitOfWork.Exercises.Add(exercise);
            unitOfWork.Commit();
        }

        public Exercise Create()
        {
            var exercise = new Exercise()
            {
                Title = $"New Exercise Item - {DateTime.Now}",
                DifficultyRating = 0,
                PracticalityRating = 0
            };
            return exercise;
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var exercise = unitOfWork.Exercises.Get(id);
            unitOfWork.Exercises.Remove(exercise);
            unitOfWork.Commit();
        }

        public IEnumerable<Exercise> Find(ExerciseSearchCriteria searchCriteria)
        {
            return unitOfWork.Exercises.Find(searchCriteria);
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
