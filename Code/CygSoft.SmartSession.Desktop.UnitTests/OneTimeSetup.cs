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
                cfg.CreateMap<ExerciseModel, Exercise>();
                cfg.CreateMap<Exercise, ExerciseModel>();
            });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
