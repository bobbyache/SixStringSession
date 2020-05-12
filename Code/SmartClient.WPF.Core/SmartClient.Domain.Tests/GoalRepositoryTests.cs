using Moq;
using Moq.Protected;
using SmartClient.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartClient.Domain.Tests
{
    public class GoalRepositoryTests
    {
        [Fact]
        public void GoalRepository_Open_WhenFileExists_ReadsFile()
        {
            var repository = new OpenedRepository();
            repository.Exists = true;

            repository.Open(@"\\some\path.json");

            Assert.False(repository.FileCreated);
            Assert.True(repository.FileRead);
        }

        [Fact]
        public void GoalRepository_Open_WhenFileDoesNotExist_CreatesFile()
        {
            var repository = new OpenedRepository();
            repository.Exists = false;

            repository.Open(@"\\some\path.json");

            Assert.True(repository.FileCreated);
            Assert.False(repository.FileRead);
        }
    }

    public class OpenedRepository : GoalRepository
    {
        public bool Exists = false;
        public bool FileCreated = false;
        public bool FileRead = false;

        protected override bool FileExists(string filePath)
        {
            return Exists;
        }

        protected override IDataGoal Read(string filePath)
        {
            FileRead = true;
            return null;
        }

        protected override IDataGoal Create(string filePath)
        {
            FileCreated = true;
            return null;
        }
    }
}
