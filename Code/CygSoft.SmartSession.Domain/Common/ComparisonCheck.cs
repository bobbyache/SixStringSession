//namespace CygSoft.SmartSession.Domain.Common
//{
//    internal class ComparisonCheck
//    {
//        public bool IsSatisfiedBy(int? constraintValue, int actualValue, ComparisonOperators comparison)
//        {
//            if (!constraintValue.HasValue)
//                return true;

//            switch (comparison)
//            {
//                case ComparisonOperators.LessThan:
//                    return actualValue < constraintValue.Value;

//                case ComparisonOperators.LessThanOrEqualTo:
//                    return actualValue <= constraintValue.Value;

//                case ComparisonOperators.GreaterThan:
//                    return actualValue > constraintValue.Value;

//                case ComparisonOperators.GreaterThanOrEqualTo:
//                    return actualValue >= constraintValue.Value;

//                default:
//                    return false;
//            }
//        }

//        public override Expression<Func<Exercise, bool>> ToExpression()
//        {
//            return exercise => new ComparisonCheck()
//                .IsSatisfiedBy(optimalDuration, exercise.OptimalDuration, comparison);
//        }

//        public bool IsSatisfiedBy(int? constraintValue, int actualValue, ComparisonOperators comparison)
//        {
//            if (!constraintValue.HasValue)
//                return true;

//            switch (comparison)
//            {
//                case ComparisonOperators.LessThan:
//                    return actualValue < constraintValue.Value;

//                case ComparisonOperators.LessThanOrEqualTo:
//                    return actualValue <= constraintValue.Value;

//                case ComparisonOperators.GreaterThan:
//                    return actualValue > constraintValue.Value;

//                case ComparisonOperators.GreaterThanOrEqualTo:
//                    return actualValue >= constraintValue.Value;

//                default:
//                    return false;
//            }
//        }

//    }
//}
