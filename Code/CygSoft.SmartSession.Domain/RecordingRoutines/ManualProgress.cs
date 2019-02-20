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

        public void Decrease(int value)
        {
            Value -= value;
        }

        public void Increase(int value)
        {
            Value += value;
        }
    }
}
