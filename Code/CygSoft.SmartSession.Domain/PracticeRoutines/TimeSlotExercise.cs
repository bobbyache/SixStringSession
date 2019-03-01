using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class TimeSlotExercise
    {
        public int Id { get; set; }
        public int TimeSlotId { get; set; }
        public string Title { get; }
        public int FrequencyWeighting { get; set; }

        public TimeSlotExercise(int id, string title, int frequencyWeighting)
        {
            Id = id;
            Title = title;
            FrequencyWeighting = frequencyWeighting;
        }

        public TimeSlotExercise(int id, int timeSlotId, string title, int frequencyWeighting)
        {
            Id = id;
            TimeSlotId = timeSlotId;
            Title = title;
            FrequencyWeighting = frequencyWeighting;
        }
    }
}
