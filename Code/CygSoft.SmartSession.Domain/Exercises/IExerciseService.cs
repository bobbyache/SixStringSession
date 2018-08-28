﻿using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExerciseService
    {
        Exercise Get(int id);
        IEnumerable<Exercise> Find(string titleFragment);
        void Delete(int id);
        void Add(Exercise exercise);
    }
}
