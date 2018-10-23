using AutoMapper;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Domain.Exercises;
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
            });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
