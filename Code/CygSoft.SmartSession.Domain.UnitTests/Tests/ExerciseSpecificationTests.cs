using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseSpecificationTests
    {
        [Test]
        public void ExerciseTitleSpecification_When_Passed_BeginningPart_Finds_Correctly()
        {
            var exercise = new Exercise
            {
                Id = 34,
                Title = "STARTING FROM THIS SPOT"
            };
            var spec = new ExerciseTitleSpecification("Starting");
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_When_Passed_EndingPart_Finds_Correctly()
        {
            var exercise = new Exercise
            {
                Id = 34,
                Title = "STARTING FROM THIS SPOT"
            };
            var spec = new ExerciseTitleSpecification("this spot");
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_When_Passed_NonMatching_Returns_False()
        {
            var exercise = new Exercise
            {
                Id = 34,
                Title = "STARTING FROM THIS SPOT"
            };
            var spec = new ExerciseTitleSpecification("Shark boy");
            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [TestCase("2018-03-11", "2018-03-13")]
        [TestCase(null, "2018-03-13")]
        [TestCase("2018-03-11", null)]
        public void ExerciseTitleSpecification_SatisfiedBy_CreateDate_Range_Finds_Correctly(string startDate, string endDate)
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateCreatedRangeSpecification(ParseDate(startDate), ParseDate(endDate));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [TestCase("2018-03-10", "2018-03-11")]
        [TestCase(null, "2018-03-11")]
        [TestCase("2018-03-13", null)]
        public void ExerciseTitleSpecification_Not_SatisfiedBy_CreateDate_Range_Will_Not_Find(string startDate, string endDate)
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateCreatedRangeSpecification(ParseDate(startDate), ParseDate(endDate));
            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_SatisfiedBy_No_CreateDate_Range_Finds_Correctly()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateCreatedRangeSpecification(null, null);
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [TestCase("2018-03-11", "2018-03-13")]
        [TestCase(null, "2018-03-13")]
        [TestCase("2018-03-11", null)]
        public void ExerciseTitleSpecification_SatisfiedBy_ModifiedDate_Range_Finds_Correctly(string startDate, string endDate)
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateModifiedRangeSpecification(ParseDate(startDate), ParseDate(endDate));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_SatisfiedBy_No_ModifiedDate_Range_Finds_Correctly()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateModifiedRangeSpecification(null, null);
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [TestCase("2018-03-10", "2018-03-11")]
        [TestCase(null, "2018-03-11")]
        [TestCase("2018-03-13", null)]
        public void ExerciseTitleSpecification_Not_SatisfiedBy_ModifiedDate_Range_Will_Not_Find(string startDate, string endDate)
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12)
            };

            var spec = new DateModifiedRangeSpecification(ParseDate(startDate), ParseDate(endDate));
            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecication_Not_Satisfied_By_Both_DateCreated_DateModified_Will_Not_Find()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12),
                DateModified = new DateTime(2018, 3, 15)
            };

            // should be satisfied...
            var dateCreatedSpec = new DateCreatedRangeSpecification(new DateTime(2018, 1, 1), new DateTime(2018,6, 1));
            // should not be satified.
            var dateModifiedSpec = new DateCreatedRangeSpecification(new DateTime(2018, 3, 16), new DateTime(2018, 6, 1));

            var spec = dateCreatedSpec.And(dateModifiedSpec);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        private DateTime? ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;
            else
                return DateTime.Parse(date);
        }

        //[Test]
        //public void ExerciseTitleSpecification_
    }
}
