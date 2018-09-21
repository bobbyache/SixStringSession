using CygSoft.SmartSession.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Keywords
{
    public class ExerciseKeyword
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
