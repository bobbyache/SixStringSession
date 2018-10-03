using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.EF.Repositories
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(SmartSessionContext context) : base(context) { }

        public override IReadOnlyList<Exercise> Find(Specification<Exercise> specification, int page = 0, int pageSize = 100)
        {
            return context.Exercises.Include(ex => ex.ExerciseKeywords).Where(ex => specification.IsSatisfiedBy(ex)).ToList();
            //    //return session.Query<T>()
            //    //    .Where(specification.ToExpression())
            //    //    .Skip(page * pageSize)
            //    //    .Take(pageSize)
            //    //    .ToList();
        }

        //public virtual IReadOnlyList<Exercise> Find(Specification<Exercise> specification, string[] keywords, int page = 0, int pageSize = 100)
        //{
        //    var exercises = context.Exercises.Where(ex => specification.IsSatisfiedBy(ex) && ContainsAllKeywords(ex, keywords[]).ToList();
        //}

        //private bool ContainsAllKeywords(Exercise exercise, string[] keywords)
        //{
        //    return exercise.ExerciseKeywords.Select(ex => ex.Keyword.Word).Except(keywords).Any();
        //    //if (exercise.ExerciseKeywords)
        //}
    }
}
