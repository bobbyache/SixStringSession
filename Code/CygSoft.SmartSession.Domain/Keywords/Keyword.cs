using CygSoft.SmartSession.Domain.Exercises;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.Keywords
{
    public class Keyword
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Word { get; set; }

        public List<ExerciseKeyword> KeywordExercises { get; set; }
    }
}
