using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class Exercise
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2, TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        public int DifficultyRating { get; set; }
        [Column(Order = 3)]
        public int OptimalDuration { get; set; }
        [Column(Order = 4)]
        public int PracticalityRating { get; set; }
        [Column(Order = 5)]
        public bool Scribed { get; set; }
        [Column(Order = 6, TypeName = "nvarchar(1000)")]
        public string Notes { get; set; }
        [Column(Order = 7)]
        public DateTime DateCreated { get; set; }
        [Column(Order = 8)]
        public DateTime DateModified { get; set; }
        

    }
}
