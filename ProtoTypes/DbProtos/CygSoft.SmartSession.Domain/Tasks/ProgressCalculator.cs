using CygSoft.SmartSession.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Tasks
{
    public class ProgressCalculator
    {
        private List<IWeightedEntity> weightedEntities = new List<IWeightedEntity>();

        public void Add(IWeightedEntity item)
        {
            weightedEntities.Add(item);
        }

        public double CalculateTotalProgress()
        {
            if (weightedEntities.Count == 0)
                return 0;

            var countOfWeightings = weightedEntities.Count;
            var sumOfWeightings = weightedEntities.Sum(w => w.Weighting);

            return weightedEntities.Select(w => CalculateWeightedValue(w, countOfWeightings, sumOfWeightings)).Sum();
        }

        private double CalculateWeightedValue(IWeightedEntity item, int countOfItems, int sumOfItemWeightings)
        {
            double weightingSlice = (((double)item.Weighting / sumOfItemWeightings) * 100);
            double val = (item.PercentCompleted / 100d) * weightingSlice;
            return val;
        }
    }
}
