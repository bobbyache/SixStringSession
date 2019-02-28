﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class TimeSlotExercise
    {
        public int Id { get; }
        public int TimeSlotId { get; }
        public string Title { get; }
        public int FrequencyWeighting { get; }

        public TimeSlotExercise(int id, string title, int frequencyWeighting)
        {
            Id = id;
            Title = title;
            FrequencyWeighting = frequencyWeighting;
        }
    }
}
