using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutine : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        public List<PracticeRoutineExercise> PracticeRoutineExercises { get; set; } = new List<PracticeRoutineExercise>();

        public PracticeRoutineExercise AddExercise(int exerciseId, int assignedPracticeTime, int difficultyRating, int practicalityRating)
        {
            var routine = new PracticeRoutineExercise()
            {
                ExerciseId = exerciseId,
                AssignedPracticeTime = assignedPracticeTime,
                DifficultyRating = difficultyRating,
                PracticalityRating = practicalityRating
            };

            PracticeRoutineExercises.Add(routine);
            return routine;
        }

        public void RemoveExercise(int exerciseId)
        {
            var routineExercise = PracticeRoutineExercises.Where(a => a.ExerciseId == exerciseId).SingleOrDefault();
            if (routineExercise != null)
            {
                RemoveExercise(routineExercise);
            }
        }

        public void RemoveExercise(PracticeRoutineExercise routineExercise)
        {
            PracticeRoutineExercises.Remove(routineExercise);
        }
    }
}
