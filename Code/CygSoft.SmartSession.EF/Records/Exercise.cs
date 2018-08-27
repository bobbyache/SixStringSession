using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSession.Domain.Records
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int DifficultyRating { get; set; }
        public int RequiredDuration { get; set; }
    }
}
