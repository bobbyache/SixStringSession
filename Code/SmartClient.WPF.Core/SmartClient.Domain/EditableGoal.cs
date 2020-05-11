using SmartClient.Domain.Data;
using SmartClient.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public class EditableGoal : IEditableGoal
    {
        private readonly IDataGoal dataGoal;

        public EditableGoal(IDataGoal dataGoal)
        {
            this.dataGoal = dataGoal;
        }

        public string Id => this.dataGoal.Id;

        public string Title
        {
            get { return this.dataGoal.Title; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                    throw new InvalidTitleException("Invalid title");
                this.dataGoal.Title = value;
            }
        }

        public double Weighting
        {
            get { return this.dataGoal.Weighting; }
            set
            {
                if (value < 0 || value > 1)

                    throw new InvalidWeightingException("Weighting must be between 0 and 1");
                this.dataGoal.Weighting = value;
            }
        }
    }
}
