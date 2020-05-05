using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSession.Domain
{
    public interface IWeightedEntity
    {
        double Weighting { get; }
        double PercentCompleted();
    }
}
