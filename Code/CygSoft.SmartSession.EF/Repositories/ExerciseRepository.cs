using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
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
            return context.Exercises.Where(ex => specification.IsSatisfiedBy(ex)).ToList();
            //    //return session.Query<T>()
            //    //    .Where(specification.ToExpression())
            //    //    .Skip(page * pageSize)
            //    //    .Take(pageSize)
            //    //    .ToList();
        }
    }
}
