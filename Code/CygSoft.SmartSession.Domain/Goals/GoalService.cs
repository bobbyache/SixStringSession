using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.Goals
{
    public class GoalService : IGoalService
    {
        private IUnitOfWork unitOfWork;

        public GoalService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
        }

        public void Add(Goal goal)
        {
            if (goal.Id > 0)
                throw new ArgumentException("A new goal cannot have an id");

            goal.DateCreated = DateTime.Now;
            goal.DateModified = goal.DateCreated;

            unitOfWork.Goals.Add(goal);
            unitOfWork.Commit();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var goal = unitOfWork.Goals.Get(id);
            unitOfWork.Goals.Remove(goal);
            unitOfWork.Commit();
        }

        public IEnumerable<Goal> Find(GoalSearchCriteria searchCriteria)
        {
            return unitOfWork.Goals.Find(searchCriteria);
        }

        public Goal Get(int id)
        {
            return unitOfWork.Goals.Get(id);
        }

        public void Update(Goal goal)
        {
            if (goal.Id <= 0)
                throw new ArgumentException("An existing goal must have an id");

            goal.DateModified = DateTime.Now;

            unitOfWork.Goals.Update(goal);
            unitOfWork.Commit();
        }
    }
}
