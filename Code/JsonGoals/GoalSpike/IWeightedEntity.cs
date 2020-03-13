using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDb
{
    public interface IWeightedEntity
    {
        int Weighting { get; }
        double PercentCompleted();
    }
}
