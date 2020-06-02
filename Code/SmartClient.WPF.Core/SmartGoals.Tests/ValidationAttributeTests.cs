using SmartGoals.Supports.Validators;
using Xunit;

namespace SmartGoals.Tests
{
    public class ValidationAttributeTests
    {
        [Fact]
        public void TestValidFilePathValidator()
        {
            ValidFilePathValidator validator = new ValidFilePathValidator();
            Assert.True(validator.IsValid(@"C:\somedirectory\myfile.txt"));
            Assert.False(validator.IsValid(@"C:\somedirectory\myfile"));
            Assert.False(validator.IsValid(@"C:\somedirectory\"));
            Assert.False(validator.IsValid(@"C:\somedirectory"));
            Assert.False(validator.IsValid(" "));
            Assert.False(validator.IsValid(""));
            Assert.False(validator.IsValid(@"C:\"));
            Assert.False(validator.IsValid("C:"));
            Assert.False(validator.IsValid(null));
            Assert.False(validator.IsValid("myfile.txt"));
        }
    }
}
