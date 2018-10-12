using CygSoft.SmartSession.Domain.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class FileServiceTests
    {
        [Test]
        public void FileService_ExerciseFolder_Is_Correctly_Resolved()
        {
            var fileService = new FileService(@"C:\FileFolder");
            Assert.That(fileService.ExerciseFolderPath, Is.EqualTo(@"C:\FileFolder\Exercises"));
        }
    }
}
