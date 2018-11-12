using CygSoft.SmartSession.Domain.Exercises;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.UnitTests.Tests
{
    [TestFixture]
    public class ExerciseSearchCriteriaTests
    {
        [Test]
        public void ExerciseSearchCriteria_Exercise_Date_DateCreatedAfter_IsSatisfied()
        {
            var exercise = new Exercise();
            exercise.Id = 1;
            exercise.DateCreated = DateTime.Parse("2018/03/01");

            var searchCriteria = new ExerciseSearchCriteria();
            searchCriteria.FromDateCreated = DateTime.Parse("2018/02/28");

            var specification = searchCriteria.Specification();
            Assert.That(specification.IsSatisfiedBy(exercise), Is.True);
        }

        [Test]
        public void ExerciseSearchCriteria_Exercise_TargetPracticeTime_SmallerThan_Criteria_IsSatisfied()
        {
            var exercise = new Exercise();
            exercise.Id = 1;
            exercise.DateCreated = DateTime.Parse("2018/03/01");
            exercise.DateModified = DateTime.Parse("2018/03/01");
            exercise.TargetMetronomeSpeed = 95;
            exercise.TargetPracticeTime = 2000;
            exercise.DifficultyRating = 4;

            var searchCriteria = new ExerciseSearchCriteria();
            searchCriteria.TargetMetronomeSpeed = 90;
            searchCriteria.TargetMetronomeSpeedOperator = Common.ComparisonOperators.GreaterThan;
            searchCriteria.TargetPracticeTime = 3000;
            searchCriteria.TargetPracticeTimeOperator = Common.ComparisonOperators.LessThan;
            searchCriteria.DifficultyRating = 3;
            searchCriteria.DifficultyRatingOperator = Common.ComparisonOperators.GreaterThan;

            var specification = searchCriteria.Specification();
            Assert.That(specification.IsSatisfiedBy(exercise), Is.True);
        }
    }
}
