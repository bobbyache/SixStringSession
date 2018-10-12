using AutoMapper;
using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Domain.Attachments;
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
                cfg.CreateMap<FileAttachmentModel, FileAttachment>();
                cfg.CreateMap<FileAttachment, FileAttachmentModel>();
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
