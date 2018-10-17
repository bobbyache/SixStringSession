using CygSoft.SmartSession.Desktop.Supports.Converters;
using CygSoft.SmartSession.Domain.Common;
using NUnit.Framework;
using System.Globalization;

namespace CygSoft.SmartSession.Desktop.UnitTests.Converters
{
    [TestFixture]
    public class ComparisonOperatorConverterTests
    {
        [Test]
        public void ComparisonOperatorConverter_Convert_Greater_Returns_Correct_TextOutput()
        {
            var comparisonOperator = ComparisonOperators.GreaterThan;
            var converter = new ComparisonOperatorConverter();
            var text = (string)converter.Convert(comparisonOperator, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.That(text, Is.EqualTo(">"));
        }

        [Test]
        public void ComparisonOperatorConverter_Convert_GreaterThanOrEqual_Returns_Correct_TextOutput()
        {
            var comparisonOperator = ComparisonOperators.GreaterThanOrEqualTo;
            var converter = new ComparisonOperatorConverter();
            var text = (string)converter.Convert(comparisonOperator, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.That(text, Is.EqualTo(">="));
        }
        [Test]
        public void ComparisonOperatorConverter_Convert_LessThan_Returns_Correct_TextOutput()
        {
            var comparisonOperator = ComparisonOperators.LessThan;
            var converter = new ComparisonOperatorConverter();
            var text = (string)converter.Convert(comparisonOperator, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.That(text, Is.EqualTo("<"));
        }
        [Test]
        public void ComparisonOperatorConverter_Convert_LessThanOrEqual_Returns_Correct_TextOutput()
        {
            var comparisonOperator = ComparisonOperators.LessThanOrEqualTo;
            var converter = new ComparisonOperatorConverter();
            var text = (string)converter.Convert(comparisonOperator, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.That(text, Is.EqualTo("<="));
        }
        [Test]
        public void ComparisonOperatorConverter_Convert_Undefined_Returns_Correct_TextOutput()
        {
            var comparisonOperator = ComparisonOperators.Undefined;
            var converter = new ComparisonOperatorConverter();
            var text = (string)converter.Convert(comparisonOperator, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.That(text, Is.EqualTo(""));
        }

        [Test]
        public void ComparisonOperatorConverter_ConvertBack_Greater_Returns_Correct_ComparisonOperator()
        {
            var converter = new ComparisonOperatorConverter();
            var op = (ComparisonOperators)converter.ConvertBack(">", typeof(ComparisonOperators), null, CultureInfo.CurrentCulture);
            Assert.That(op, Is.EqualTo(ComparisonOperators.GreaterThan));
        }

        [Test]
        public void ComparisonOperatorConverter_ConvertBack_GreaterThanOrEqual_Returns_Correct_ComparisonOperator()
        {
            var converter = new ComparisonOperatorConverter();
            var op = (ComparisonOperators)converter.ConvertBack(">=", typeof(ComparisonOperators), null, CultureInfo.CurrentCulture);
            Assert.That(op, Is.EqualTo(ComparisonOperators.GreaterThanOrEqualTo));
        }

        [Test]
        public void ComparisonOperatorConverter_ConvertBack_LessThan_Returns_Correct_ComparisonOperator()
        {
            var converter = new ComparisonOperatorConverter();
            var op = (ComparisonOperators)converter.ConvertBack("<", typeof(ComparisonOperators), null, CultureInfo.CurrentCulture);
            Assert.That(op, Is.EqualTo(ComparisonOperators.LessThan));
        }

        [Test]
        public void ComparisonOperatorConverter_ConvertBack_LessThanOrEqual_Returns_Correct_ComparisonOperator()
        {
            var converter = new ComparisonOperatorConverter();
            var op = (ComparisonOperators)converter.ConvertBack("<=", typeof(ComparisonOperators), null, CultureInfo.CurrentCulture);
            Assert.That(op, Is.EqualTo(ComparisonOperators.LessThanOrEqualTo));
        }

        [Test]
        public void ComparisonOperatorConverter_ConvertBack_Undefined_Returns_Correct_ComparisonOperator()
        {
            var converter = new ComparisonOperatorConverter();
            var op = (ComparisonOperators)converter.ConvertBack("", typeof(ComparisonOperators), null, CultureInfo.CurrentCulture);
            Assert.That(op, Is.EqualTo(ComparisonOperators.Undefined));
        }
    }
}
