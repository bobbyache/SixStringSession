using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Exercises
{
    public class ExerciseSearchCriteria : IExerciseSearchCriteria
    {
        public DateTime? FromDateCreated { get; set; }
        public DateTime? ToDateCreated { get; set; }
        public DateTime? FromDateModified { get; set; }
        public DateTime? ToDateModified { get; set; }
        public int? TargetMetronomeSpeed { get; set; }
        public int? TargetPracticeTime { get; set; }
        public int? DifficultyRating { get; set; }
        public ComparisonOperators TargetMetronomeSpeedOperator { get; set; }
        public ComparisonOperators TargetPracticeTimeOperator { get; set; }
        public ComparisonOperators DifficultyRatingOperator { get; set; }
        public ComparisonOperators PracticalityRatingOperator { get; set; }
        public int? PracticalityRating { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }

        public string[] KeywordSpecification()
        {
            return Keywords.Split(new char[] { ',' })
                .Select(k => k.Trim().ToUpper())
                .Where(k => !string.IsNullOrWhiteSpace(k)).ToArray();
        }

        public Specification<Exercise> Specification()
        {
            return
                new ExerciseTitleSpecification(Title)
                .And(new ExerciseDateModifiedSpecification(FromDateModified, ToDateModified))
                .And(new ExerciseDateCreatedSpecification(FromDateCreated, ToDateCreated))
                .And(new ExerciseTargetMetronomeSpeedSpecification(TargetMetronomeSpeed, TargetMetronomeSpeedOperator))
                .And(new ExerciseTargetPracticeTimeSpecification(TargetPracticeTime, TargetPracticeTimeOperator))
                .And(new ExerciseDifficultyRatingSpecification(DifficultyRating, DifficultyRatingOperator))
                .And(new ExercisePracticalityRatingSpecification(PracticalityRating, PracticalityRatingOperator))
                //.And(new ExerciseKeywordSpecification(KeywordSpecification()));
            ;
        }
    }
}
