using AutoMapper;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using NUnit.Framework;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [SetUpFixture]
    public class OneTimeSetup
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ExerciseEditViewModel, Exercise>();
                cfg.CreateMap<Exercise, ExerciseEditViewModel>();

                cfg.CreateMap<PracticeRoutineEditViewModel, PracticeRoutine>();
                cfg.CreateMap<PracticeRoutine, PracticeRoutineEditViewModel>();
                //cfg.CreateMap<PracticeRoutine, PracticeRoutine>();
            });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
