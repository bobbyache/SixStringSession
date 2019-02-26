using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Recording;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutineService : IPracticeRoutineService
    {
        private IUnitOfWork unitOfWork;
        private IExerciseService exerciseService;

        public PracticeRoutineService(IUnitOfWork unitOfWork, IExerciseService exerciseService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("ExerciseService must be provided.");
        }

        public PracticeRoutine Create()
        {
            var practiceRoutine = new PracticeRoutine()
            {
                Title = $"New Practice Routine - {DateTime.Now}"
            };
            return practiceRoutine;
        }

        public void Add(PracticeRoutine practiceRoutine)
        {
            if (practiceRoutine.Id > 0)
                throw new ArgumentException("A new practiceRoutine cannot have an id");

            unitOfWork.PracticeRoutines.Add(practiceRoutine);
            unitOfWork.Commit();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var practiceRoutine = unitOfWork.PracticeRoutines.Get(id);
            unitOfWork.PracticeRoutines.Remove(practiceRoutine);
            unitOfWork.Commit();
        }

        public IEnumerable<PracticeRoutine> Find(PracticeRoutineSearchCriteria searchCriteria)
        {
            return unitOfWork.PracticeRoutines.Find(searchCriteria);
        }

        public PracticeRoutine Get(int id)
        {
            return unitOfWork.PracticeRoutines.Get(id);
        }

        public void Update(PracticeRoutine practiceRoutine)
        {
            if (practiceRoutine.Id <= 0)
                throw new ArgumentException("An existing practiceRoutine must have an id");

            unitOfWork.PracticeRoutines.Update(practiceRoutine);
            unitOfWork.Commit();
        }

        public PracticeRoutineExercise CreatePracticeRoutineExerciseFor(int exerciseId)
        {
            var exercise = exerciseService.Get(exerciseId);
            var routineExercise = new PracticeRoutineExercise
            {
                ExerciseId = exerciseId,
                Title = exercise.Title,
                FrequencyWeighting = 0,
                AssignedPracticeTime = 300
            };

            return routineExercise;
        }

        public PracticeRoutineRecorder GetPracticeRoutineRecorder(int id)
        {
            return unitOfWork.PracticeRoutines.GetPracticeRoutineRecorder(id);
        }
    }
}
