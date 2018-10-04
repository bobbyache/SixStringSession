using AutoMapper;
using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Domain.Attachments;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
