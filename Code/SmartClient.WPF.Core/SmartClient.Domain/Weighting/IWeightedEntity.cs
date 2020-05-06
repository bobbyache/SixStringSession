using System;
using System.Collections.Generic;
using System.Text;

namespace SmartClient.Domain.Weighting
{
    public interface IWeightedEntity
    {
        double Weighting { get; }
        int PercentProgress { get; }
    }
}
