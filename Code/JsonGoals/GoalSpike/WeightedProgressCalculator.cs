using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonDb
{
    public class WeightedProgressCalculator
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

            var result = weightedEntities.Select(w => CalculateWeightedValue(w, countOfWeightings, sumOfWeightings)).Sum();

            return result;
        }


        private double CalculateWeightedValue(IWeightedEntity item, int countOfItems, int sumOfItemWeightings)
        {
            double weightingSlice = (((double)item.Weighting / sumOfItemWeightings) * 100);
            double val = (item.PercentCompleted() / 100d) * weightingSlice;
            return val;
        }
    }
}
