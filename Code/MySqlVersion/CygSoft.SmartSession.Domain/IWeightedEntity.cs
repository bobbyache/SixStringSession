using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public interface IWeightedEntity
    {
        string Id { get; }
        double Weighting { get; }

        double SetWeighting(double newWeighting);
    }
}
