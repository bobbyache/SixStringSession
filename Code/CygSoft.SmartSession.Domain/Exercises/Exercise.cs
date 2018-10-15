using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Keywords;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class Exercise : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
        public int DifficultyRating { get; set; }
        public int OptimalDuration { get; set; }
        public int PracticalityRating { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }

        public List<ExerciseKeyword> ExerciseKeywords { get; set; }
    }
}
