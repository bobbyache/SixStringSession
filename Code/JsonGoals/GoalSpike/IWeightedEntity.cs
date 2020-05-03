using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDb
{
    public interface IWeightedEntity
    {
        double Weighting { get; }
        double PercentCompleted();
    }
}
