using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int DifficultyRating { get; set; }
        public int OptimalDuration { get; set; }
        public int PracticalityRating { get; set; }
        public bool Scribed { get; set; }
        public string Notes { get; set; }
    }
}
