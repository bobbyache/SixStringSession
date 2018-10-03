using System;
using System.Linq.Expressions;

// https://enterprisecraftsmanship.com/2016/02/08/specification-pattern-c-implementation/
//  Use IsSatisfiedBy "in memory".
//  Use ToExpression() when querying through a repository.

// https://github.com/vkhorikov/SpecificationPattern

namespace CygSoft.SmartSession.Domain.Common
{
    public abstract class Specification<T>
    {

        public abstract Expression<Func<T, bool>> ToExpression();


        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }


        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }


        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
    }
}
