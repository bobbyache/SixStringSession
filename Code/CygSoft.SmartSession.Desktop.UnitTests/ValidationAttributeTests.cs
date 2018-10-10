using CygSoft.SmartSession.Desktop.Supports.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class ValidationAttributeTests
    {
        [Test]
        public void TestValidFilePathValidator()
        {
            ValidFilePathValidator validator = new ValidFilePathValidator();
            Assert.IsTrue(validator.IsValid(@"C:\somedirectory\myfile.txt"));
            Assert.IsFalse(validator.IsValid(@"C:\somedirectory\myfile"));
            Assert.IsFalse(validator.IsValid(@"C:\somedirectory\"));
            Assert.IsFalse(validator.IsValid(@"C:\somedirectory"));
            Assert.IsFalse(validator.IsValid(" "));
            Assert.IsFalse(validator.IsValid(""));
            Assert.IsFalse(validator.IsValid(@"C:\"));
            Assert.IsFalse(validator.IsValid("C:"));
            Assert.IsFalse(validator.IsValid(null));
            Assert.IsFalse(validator.IsValid("myfile.txt"));
        }

        [Test]
        public void TestFilenameAttribute()
        {
            var rxa = new ValidFileNameAttribute();
            Assert.IsFalse(rxa.IsValid("pptx."));
            Assert.IsFalse(rxa.IsValid("pp.tx."));
            Assert.IsFalse(rxa.IsValid("."));
            Assert.IsFalse(rxa.IsValid(".pp.tx"));
            Assert.IsFalse(rxa.IsValid(".pptx"));
            Assert.IsFalse(rxa.IsValid("pptx"));
            Assert.IsFalse(rxa.IsValid("a/abc.pptx"));
            Assert.IsFalse(rxa.IsValid("a\\abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c:abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c<abc.pptx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
            rxa = new ValidFileNameAttribute { AllowedExtensions = ".pptx" };
            Assert.IsFalse(rxa.IsValid("abc.docx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
        }


        [Test]
        public void TestFilenameAttribute_2()
        {
            var rxa = new ValidFileNameAttribute();
            rxa.RequireExtension = false;

            Assert.IsFalse(rxa.IsValid("pptx."));
            Assert.IsFalse(rxa.IsValid("pp.tx."));
            Assert.IsFalse(rxa.IsValid("."));
            Assert.IsFalse(rxa.IsValid(".pp.tx"));
            Assert.IsFalse(rxa.IsValid(".pptx"));
            Assert.IsTrue(rxa.IsValid("pptx"));
            Assert.IsFalse(rxa.IsValid("a/abc.pptx"));
            Assert.IsFalse(rxa.IsValid("a\\abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c:abc.pptx"));
            Assert.IsFalse(rxa.IsValid("c<abc.pptx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
            rxa = new ValidFileNameAttribute { AllowedExtensions = ".pptx" };
            Assert.IsFalse(rxa.IsValid("abc.docx"));
            Assert.IsTrue(rxa.IsValid("abc.pptx"));
        }
    }
}
