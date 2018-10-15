using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.Goals.Specifications
{
    public class GoalDateCreatedSpecification : Specification<Goal>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public GoalDateCreatedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<Goal, bool>> ToExpression()
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate && ex.DateCreated <= endDate;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                return ex => true;

            }
            else if (startDate.HasValue)
            {
                return ex => ex.DateCreated >= startDate;
            }
            else if (endDate.HasValue)
            {
                return ex => ex.DateCreated <= endDate;
            }
            else
            {
                return ex => false;
            }
        }
    }

    public class GoalDateModifiedSpecification : Specification<Goal>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public GoalDateModifiedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<Goal, bool>> ToExpression()
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                return ex => ex.DateModified >= startDate && ex.DateModified <= endDate;
            }
            else if (!startDate.HasValue && !endDate.HasValue)
            {
                return ex => true;
            }
            else if (startDate.HasValue)
            {
                return ex => ex.DateModified >= startDate;
            }
            else if (endDate.HasValue)
            {
                return ex => ex.DateModified <= endDate;
            }
            else
            {
                return ex => false;
            }
        }
    }

    public class GoalHasNotesSpecification : Specification<Goal>
    {
        private readonly bool? hasNotes;

        public GoalHasNotesSpecification(bool? hasNotes)
        {
            this.hasNotes = hasNotes;
        }

        public override Expression<Func<Goal, bool>> ToExpression()
        {
            if (hasNotes == null)
                return ex => true;

            else
                return ex => string.IsNullOrWhiteSpace(ex.Notes) == !hasNotes;
        }
    }

    public class GoalTitleSpecification : Specification<Goal>
    {
        private readonly string titleFragment;

        public GoalTitleSpecification(string titleFragment)
        {
            this.titleFragment = titleFragment;
        }

        public override Expression<Func<Goal, bool>> ToExpression()
        {
            if (string.IsNullOrEmpty(titleFragment))
                return ex => true;
            else
                return ex => ex.Title.ToUpper().Contains(titleFragment.ToUpper());
        }

        private bool ContainsText(Goal goal, string titleFragment)
        {
            if (string.IsNullOrEmpty(titleFragment))
                return true;

            return goal.Title.ToUpper().Contains(titleFragment.ToUpper());
        }
    }
}
