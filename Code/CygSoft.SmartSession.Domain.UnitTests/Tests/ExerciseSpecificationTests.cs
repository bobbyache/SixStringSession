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

        [Test]
        public void ExerciseTitleSpecification_When_Passed_Null_Is_True()
        {
            // if a title is null or empty, don't try and filter by the field.
            var exercise = new Exercise
            {
                Id = 34,
                Title = "Lava Girl"
            };
            var spec = new ExerciseTitleSpecification(null);
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_When_Passed_EmptyText_Is_True()
        {
            // if a title is null or empty, don't try and filter by the field.
            var exercise = new Exercise
            {
                Id = 34,
                Title = "Lava Girl"
            };
            var spec = new ExerciseTitleSpecification("");
            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
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

            var spec = new ExerciseDateCreatedSpecification(ParseDate(startDate), ParseDate(endDate));
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

            var spec = new ExerciseDateCreatedSpecification(ParseDate(startDate), ParseDate(endDate));
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

            var spec = new ExerciseDateCreatedSpecification(null, null);
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
                DateModified = new DateTime(2018, 3, 12)
            };

            var spec = new ExerciseDateModifiedSpecification(ParseDate(startDate), ParseDate(endDate));
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

            var spec = new ExerciseDateModifiedSpecification(null, null);
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
                DateModified = new DateTime(2018, 3, 12)
            };

            var spec = new ExerciseDateModifiedSpecification(ParseDate(startDate), ParseDate(endDate));
            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_Not_Satisfied_By_Both_DateCreated_DateModified_Will_Not_Find()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 12),
                DateModified = new DateTime(2018, 3, 15)
            };

            // should be satisfied...
            var dateCreatedSpec = new ExerciseDateCreatedSpecification(new DateTime(2018, 1, 1), new DateTime(2018,6, 1));
            // should not be satified.
            var dateModifiedSpec = new ExerciseDateCreatedSpecification(new DateTime(2018, 3, 16), new DateTime(2018, 6, 1));

            var spec = dateCreatedSpec.And(dateModifiedSpec);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_Both_ModifiedDate_And_Title_Within_Range_Returns_True()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateModified = new DateTime(2018, 3, 15)
            };

            var spec = new ExerciseTitleSpecification("tit")
                .And(new ExerciseDateModifiedSpecification(new DateTime(2018, 3, 14), new DateTime(2018, 3, 16)));


            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseTitleSpecification_Both_CreatedDate_And_Title_Within_Range_Returns_True()
        {
            var exercise = new Exercise
            {
                Title = "Title",
                DateCreated = new DateTime(2018, 3, 15)
            };

            var spec = new ExerciseTitleSpecification("tit")
                .And(new ExerciseDateCreatedSpecification(new DateTime(2018, 3, 14), new DateTime(2018, 3, 16)));


            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_LessThan_As_Expected_Returns_True()
        {
            var exercise = new Exercise
            {
                OptimalDuration = 3
            };

            var spec = new ExerciseDurationSpecification(4, ComparisonOperators.LessThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_LessThan_ButActual_IsNot_Returns_False()
        {
            var exercise = new Exercise
            {
                OptimalDuration = 5
            };

            var spec = new ExerciseDurationSpecification(4, ComparisonOperators.LessThan);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_LessThanOrEqual_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { OptimalDuration = 3 };
            var exercise2 = new Exercise { OptimalDuration = 2 };

            var spec = new ExerciseDurationSpecification(3, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_LessThanOrEqual_ButActual_IsNot_Returns_False()
        {
            var exercise1 = new Exercise { OptimalDuration = 5 };

            var spec = new ExerciseDurationSpecification(4, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise1));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_GreaterThan_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { OptimalDuration = 3 };
            var exercise2 = new Exercise { OptimalDuration = 2 };

            var spec = new ExerciseDurationSpecification(1, ComparisonOperators.GreaterThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_GreaterThanOrEqualTo_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { OptimalDuration = 3 };
            var exercise2 = new Exercise { OptimalDuration = 2 };

            var spec = new ExerciseDurationSpecification(2, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDurationSpecification_Given_Value_OfNull_Returns_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { OptimalDuration = 2 };

            var spec = new ExerciseDurationSpecification(null, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_LessThan_As_Expected_Returns_True()
        {
            var exercise = new Exercise
            {
                DifficultyRating = 3
            };

            var spec = new ExerciseDifficultyRatingSpecification(4, ComparisonOperators.LessThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_LessThan_ButActual_IsNot_Returns_False()
        {
            var exercise = new Exercise
            {
                DifficultyRating = 5
            };

            var spec = new ExerciseDifficultyRatingSpecification(4, ComparisonOperators.LessThan);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_LessThanOrEqual_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { DifficultyRating = 3 };
            var exercise2 = new Exercise { DifficultyRating = 2 };

            var spec = new ExerciseDifficultyRatingSpecification(3, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_LessThanOrEqual_ButActual_IsNot_Returns_False()
        {
            var exercise1 = new Exercise { DifficultyRating = 5 };

            var spec = new ExerciseDifficultyRatingSpecification(4, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise1));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_GreaterThan_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { DifficultyRating = 3 };
            var exercise2 = new Exercise { DifficultyRating = 2 };

            var spec = new ExerciseDifficultyRatingSpecification(1, ComparisonOperators.GreaterThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_GreaterThanOrEqualTo_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { DifficultyRating = 3 };
            var exercise2 = new Exercise { DifficultyRating = 2 };

            var spec = new ExerciseDifficultyRatingSpecification(2, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExerciseDifficultyRatingSpecification_Given_Value_OfNull_Returns_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { DifficultyRating = 2 };

            var spec = new ExerciseDifficultyRatingSpecification(null, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }
        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_LessThan_As_Expected_Returns_True()
        {
            var exercise = new Exercise
            {
                PracticalityRating = 3
            };

            var spec = new ExercisePracticalityRatingSpecification(4, ComparisonOperators.LessThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_LessThan_ButActual_IsNot_Returns_False()
        {
            var exercise = new Exercise
            {
                PracticalityRating = 5
            };

            var spec = new ExercisePracticalityRatingSpecification(4, ComparisonOperators.LessThan);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_LessThanOrEqual_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { PracticalityRating = 3 };
            var exercise2 = new Exercise { PracticalityRating = 2 };

            var spec = new ExercisePracticalityRatingSpecification(3, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_LessThanOrEqual_ButActual_IsNot_Returns_False()
        {
            var exercise1 = new Exercise { PracticalityRating = 5 };

            var spec = new ExercisePracticalityRatingSpecification(4, ComparisonOperators.LessThanOrEqualTo);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise1));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_GreaterThan_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { PracticalityRating = 3 };
            var exercise2 = new Exercise { PracticalityRating = 2 };

            var spec = new ExercisePracticalityRatingSpecification(1, ComparisonOperators.GreaterThan);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_GreaterThanOrEqualTo_As_Expected_Returns_True()
        {
            var exercise1 = new Exercise { PracticalityRating = 3 };
            var exercise2 = new Exercise { PracticalityRating = 2 };

            var spec = new ExercisePracticalityRatingSpecification(2, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
        }

        [Test]
        public void ExercisePracticalityRatingSpecification_Given_Value_OfNull_Returns_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { PracticalityRating = 2 };

            var spec = new ExercisePracticalityRatingSpecification(null, ComparisonOperators.GreaterThanOrEqualTo);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseIsScribedSpecification_Given_A_Null_Value_Returns_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { Scribed = true };
            var spec = new ExerciseIsScribedSpecification(null);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseHasNotesSpecification_Given_A_Null_Value_Returns_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { Notes = "here is a note" };
            var spec = new ExerciseIsScribedSpecification(null);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseIsScribedSpecification_Given_A_True_Constraint_Value_Returns_True_When_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { Scribed = true };
            var spec = new ExerciseIsScribedSpecification(true);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseIsScribedSpecification_Given_A_False_Constraint_Value_Returns_False_When_True()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { Scribed = true };
            var spec = new ExerciseIsScribedSpecification(false);

            Assert.IsFalse(spec.IsSatisfiedBy(exercise));
        }







        [Test]
        public void ExerciseHasNotesSpecification_Given_A_True_Constraint_Value_Returns_True_When_Notes_Exist()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise = new Exercise { Notes = "here are some notes." };
            var spec = new ExerciseHasNotesSpecification(true);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise));
        }

        [Test]
        public void ExerciseHasNotesSpecification_Given_A_NoNotes_Constraint_Value_Returns_True_When_NoNotes_Exist()
        {
            // any null value means that we cannot constrain to this, so it must always
            // be satisfied.
            var exercise1 = new Exercise { Notes = " " };
            var exercise2 = new Exercise { Notes = "" };
            var exercise3 = new Exercise { Notes = null };

            var spec = new ExerciseHasNotesSpecification(false);

            var test = string.IsNullOrWhiteSpace(exercise1.Notes);

            Assert.IsTrue(spec.IsSatisfiedBy(exercise1));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise2));
            Assert.IsTrue(spec.IsSatisfiedBy(exercise3));
        }

        private DateTime? ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;
            else
                return DateTime.Parse(date);
        }
    }
}
