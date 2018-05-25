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
            public double Value { get; set; }
        }

        private Dictionary<string, Weighting> weightings = new Dictionary<string, Weighting>();

        public bool Exists(string id) => weightings.ContainsKey(id);

        public double this[string id] => weightings[id].Value;

        public void AddItem(string id)
        {
            weightings.Add(id, new Weighting { Id = id, Value = 0 });
            foreach (var item in weightings.Values)
            {
                item.Value = 100d / weightings.Count;
            }
        }

        public void RemoveItem(string id)
        {
            weightings.Remove(id);
        }

        public void Change(string id, double value)
        {
            weightings[id].Value = value;
        }
    }
}
