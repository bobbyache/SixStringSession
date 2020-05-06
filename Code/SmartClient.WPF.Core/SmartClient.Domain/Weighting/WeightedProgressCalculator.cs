using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartClient.Domain.Weighting
{
    public class WeightedProgressCalculator
    {
        private List<IWeightedEntity> weightedEntities = new List<IWeightedEntity>();

        public void Add(IWeightedEntity item)
        {
            weightedEntities.Add(item);
        }

        public void AddRange(IList<IWeightedEntity> items)
        {
            weightedEntities.AddRange(items);
        }

        public int CalculateTotalProgress()
        {
            if (weightedEntities.Count == 0)
                return 0;

            var countOfWeightings = weightedEntities.Count;
            var sumOfWeightings = weightedEntities.Sum(w => w.Weighting);

            var result = weightedEntities.Select(w => CalculateWeightedValue(w, countOfWeightings, sumOfWeightings)).Sum();

            return (int)Math.Round(result);
        }

        private double CalculateWeightedValue(IWeightedEntity item, int countOfItems, double sumOfItemWeightings)
        {
            double weightingSlice = ((item.Weighting / sumOfItemWeightings) * 100);
            double val = (item.PercentProgress / 100d) * weightingSlice;
            return val;
        }
    }
}
