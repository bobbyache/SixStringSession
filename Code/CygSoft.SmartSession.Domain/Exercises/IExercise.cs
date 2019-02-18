using CygSoft.SmartSession.Domain.Keywords;
using CygSoft.SmartSession.Domain.Sessions;
using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public interface IExercise : IEntity
    {
        int ManualProgressWeighting { get; set; }
        int PracticeTimeProgressWeighting { get; set; }
        int SpeedProgressWeighting { get; set; }
        int? TargetMetronomeSpeed { get; set; }
        int? TargetPracticeTime { get; set; }
        string Title { get; set; }

        double GetPracticeTimeProgress();
        double GetSpeedProgress();

        int GetLastRecordedManualProgress();
        int GetLastRecordedSpeed();
        double GetPercentComplete();
        double GetSecondsPracticed();

        List<ExerciseKeyword> ExerciseKeywords { get; set; }
        List<ExerciseActivity> ExerciseActivity { get; set; }
    }
}