using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.GoalManagement
{
    public class WeightingCalculator
    {
        private class Weighting
        {
            public string Id { get; set; }
            public int Value { get; set; }
            public double Percentage { get; set; }
        }

        private Dictionary<string, Weighting> weightings = new Dictionary<string, Weighting>();

        public bool Exists(string id) => weightings.ContainsKey(id);

        public double this[string id] => weightings[id].Percentage;

        public void Update(string id, int relativeValue)
        {
            if (weightings.ContainsKey(id))
                weightings[id].Value = relativeValue;
            else
                weightings.Add(id, new Weighting { Id = id, Value = relativeValue });

            CalculatePercentages();
        }

        private void CalculatePercentages()
        {
            int total = weightings.Values.Sum(w => w.Value);

            foreach (var item in weightings.Values)
            {
                item.Percentage = ((double)item.Value / total) * 100;
            }
        }

        public void RemoveItem(string id)
        {
            weightings.Remove(id);
            CalculatePercentages();
        }

        public void Change(string id, double value)
        {
            CalculatePercentages();
        }
    }
}
