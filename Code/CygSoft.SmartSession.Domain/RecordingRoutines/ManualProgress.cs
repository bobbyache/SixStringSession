using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.RecordingRoutines
{
    public class ManualProgress : IManualProgress
    {
        public ManualProgress(int value, int weighting)
        {
            Weighting = weighting;
            Value = value;
        }

        public int Weighting { get; }
        public int Value { get; private set; }

        public double CalculateProgress()
        {
            return Value;
        }

        public IManualProgress Decrease(int value)
        {
            var newValue = Value - value;
            if (newValue < 0) newValue = 0;

            var progress = new ManualProgress(newValue, this.Weighting);

            return progress;
        }

        public IManualProgress Increase(int value)
        {
            var newValue = Value + value;
            var progress = new ManualProgress(newValue, this.Weighting);

            return progress;
        }
    }
}
