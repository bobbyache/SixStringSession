using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.TimerApp
{
    public class Countdown
    {
        public double CountdownSeconds { get; private set; }

        public Countdown(double countdownSeconds)
        {
            CountdownSeconds = countdownSeconds;
        }

        public string GetValue(DateTime startTime, DateTime currentTime)
        {
            double totalSecondsElapsed = Math.Abs(new TimeSpan(startTime.Ticks).Subtract(new TimeSpan(currentTime.Ticks)).TotalSeconds);
            double totalSecondsRemaining = CountdownSeconds - totalSecondsElapsed;

            return DisplayTime(totalSecondsRemaining);
        }

        private string DisplayTime(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }
    }
}
