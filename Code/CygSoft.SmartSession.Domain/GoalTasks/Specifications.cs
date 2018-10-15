using CygSoft.SmartSession.Domain.Common;
using System;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Domain.GoalTasks.Specifications
{
    public class GoalTaskDateCreatedSpecification : Specification<GoalTask>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public GoalTaskDateCreatedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<GoalTask, bool>> ToExpression()
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

    public class GoalTaskDateModifiedSpecification : Specification<GoalTask>
    {
        private readonly DateTime? startDate;
        private readonly DateTime? endDate;

        public GoalTaskDateModifiedSpecification(DateTime? startDate, DateTime? endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override Expression<Func<GoalTask, bool>> ToExpression()
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

    public class GoalTaskHasNotesSpecification : Specification<GoalTask>
    {
        private readonly bool? hasNotes;

        public GoalTaskHasNotesSpecification(bool? hasNotes)
        {
            this.hasNotes = hasNotes;
        }

        public override Expression<Func<GoalTask, bool>> ToExpression()
        {
            if (hasNotes == null)
                return ex => true;

            else
                return ex => string.IsNullOrWhiteSpace(ex.Notes) == !hasNotes;
        }
    }

    public class GoalTaskTitleSpecification : Specification<GoalTask>
    {
        private readonly string titleFragment;

        public GoalTaskTitleSpecification(string titleFragment)
        {
            this.titleFragment = titleFragment;
        }

        public override Expression<Func<GoalTask, bool>> ToExpression()
        {
            if (string.IsNullOrEmpty(titleFragment))
                return ex => true;
            else
                return ex => ex.Title.ToUpper().Contains(titleFragment.ToUpper());
        }

        private bool ContainsText(GoalTask goalTask, string titleFragment)
        {
            if (string.IsNullOrEmpty(titleFragment))
                return true;

            return goalTask.Title.ToUpper().Contains(titleFragment.ToUpper());
        }
    }
}
