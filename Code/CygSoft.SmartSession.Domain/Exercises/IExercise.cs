using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExercise
    {
        int Id { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }
        int ManualProgressWeighting { get; set; }
        int PracticeTimeProgressWeighting { get; set; }
        int SpeedProgressWeighting { get; set; }
        int? TargetMetronomeSpeed { get; set; }
        int? TargetPracticeTime { get; set; }
        string Title { get; set; }

        int GetLastRecordedManualProgress();
        int GetLastRecordedSpeed();
        double GetPercentComplete();
        double GetSecondsPracticed();

        List<ExerciseKeyword> ExerciseKeywords { get; set; }
        List<ExerciseActivity> ExerciseActivity { get; set; }
    }
}