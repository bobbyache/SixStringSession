
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Domain.GoalTasks
{
    public class GoalTaskService : IGoalTaskService
    {
        private IUnitOfWork unitOfWork;

        public GoalTaskService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork must be provided.");
        }

        public void Add(GoalTask goalTask)
        {
            if (goalTask.Id > 0)
                throw new ArgumentException("A new goalTask cannot have an id");

            goalTask.DateCreated = DateTime.Now;
            goalTask.DateModified = goalTask.DateCreated;

            unitOfWork.GoalTasks.Add(goalTask);
            unitOfWork.Complete();
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new ArgumentException("The Id is invalid and must be greater than 0.");

            var goalTask = unitOfWork.GoalTasks.Get(id);
            unitOfWork.GoalTasks.Remove(goalTask);
            unitOfWork.Complete();
        }

        public IEnumerable<GoalTask> Find(GoalTaskSearchCriteria searchCriteria)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria.Keywords))
                return unitOfWork.GoalTasks.Find(searchCriteria.Specification());

            else
                return unitOfWork.GoalTasks.Find(searchCriteria.Specification(), searchCriteria.KeywordSpecification());
        }

        public GoalTask Get(int id)
        {
            return unitOfWork.GoalTasks.Get(id);
        }

        public void Update(GoalTask goalTask)
        {
            if (goalTask.Id <= 0)
                throw new ArgumentException("An existing goalTask must have an id");

            goalTask.DateModified = DateTime.Now;

            unitOfWork.GoalTasks.Update(goalTask);
            unitOfWork.Complete();
        }
    }
}
