using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseServiceTests
    {
        [Test]
        public void ExerciseService_AddFiles_Calls_FileService_AddExerciseFiles()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Exercises).Returns(new Mock<IExerciseRepository>().Object);

            var exercise = new Exercise();
            exercise.Id = 23;

            var fileService = new Mock<IFileService>();
            //fileService.Setup(s => s.FileExists(It.IsAny<string>())).Returns(false);

            var exerciseService = new ExerciseService(unitOfWork.Object, fileService.Object);
            exerciseService.AddFiles(23, new string[] { @"C:\somepath\exercise_file.gp" });

            fileService.Verify(fService => fService.AddExerciseFiles(It.IsAny<int>(), It.IsAny<string[]>()), 
                Times.Once, "IFileService.AddExerciseFiles was not called.");

        }

        [Test]
        public void ExerciseService_DeleteFiles_Calls_FileService_DeleteExerciseFiles()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Exercises).Returns(new Mock<IExerciseRepository>().Object);

            var exercise = new Exercise();
            exercise.Id = 23;

            var fileService = new Mock<IFileService>();

            var exerciseService = new ExerciseService(unitOfWork.Object, fileService.Object);
            exerciseService.DeleteFiles(23, new string[] { @"C:\somepath\exercise_file.gp" });

            fileService.Verify(fService => fService.DeleteExerciseFiles(It.IsAny<int>(), It.IsAny<string[]>()),
                Times.Once, "IFileService.DeleteExerciseFiles was not called.");

        }

        [Test]
        public void ExerciseService_DeleteFiles_Calls_FileService_DeleteExerciseFiles_All()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Exercises).Returns(new Mock<IExerciseRepository>().Object);

            var exercise = new Exercise();
            exercise.Id = 23;

            var fileService = new Mock<IFileService>();

            var exerciseService = new ExerciseService(unitOfWork.Object, fileService.Object);
            exerciseService.DeleteFiles(23);

            fileService.Verify(fService => fService.DeleteExerciseFiles(It.IsAny<int>()),
                Times.Once, "IFileService.DeleteExerciseFiles was not called.");

        }

        [Test]
        public void ExerciseService_GetFiles_Calls_FileService_GetExerciseFiles()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Exercises).Returns(new Mock<IExerciseRepository>().Object);

            var exercise = new Exercise();
            exercise.Id = 23;

            var fileService = new Mock<IFileService>();

            var exerciseService = new ExerciseService(unitOfWork.Object, fileService.Object);
            exerciseService.GetFiles(23);

            fileService.Verify(fService => fService.GetExerciseFiles(It.IsAny<int>()),
                Times.Once, "IFileService.GetExerciseFiles was not called.");

        }

        [Test]
        public void ExerciseService_OpenFile_Calls_FileService_OpenExerciseFile()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(s => s.Exercises).Returns(new Mock<IExerciseRepository>().Object);

            var exercise = new Exercise();
            exercise.Id = 23;

            var fileService = new Mock<IFileService>();

            var exerciseService = new ExerciseService(unitOfWork.Object, fileService.Object);
            exerciseService.OpenFile(23, "TestFile.gp");

            fileService.Verify(fService => fService.OpenExerciseFile(It.IsAny<int>(), It.IsAny<string>()),
                Times.Once, "IFileService.OpenExerciseFile was not called.");

        }

    }
}
