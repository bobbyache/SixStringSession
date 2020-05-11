using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain
{
    public interface IEditableGoal
    {
        string Id { get; }

        string Title { get; set; }

        public double Weighting { get; set; }
    }
}
