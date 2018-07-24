using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CygSoft.SmartSession.TimerApp
{
    public class CountdownEventArgs : EventArgs
    {
        public readonly string DisplayString;
        public readonly double RemainingSeconds;

        public CountdownEventArgs(string displayString, double remainingSeconds)
        {
            DisplayString = displayString;
            RemainingSeconds = remainingSeconds;
        }
    }

    public class Countdown
    {
        private Timer _timer;
        private DateTime _startTime = DateTime.MinValue;
        private double _bankedSeconds = 0;

        public event EventHandler<CountdownEventArgs> TickTock;
        public event EventHandler<CountdownEventArgs> Resetting;
        public event EventHandler<CountdownEventArgs> TimeUp;

        public double CountdownSeconds { get; private set; }

        public Countdown(Timer timer, double countdownSeconds)
        {
            _timer = timer;
            CountdownSeconds = countdownSeconds;
        }


        public void Start()
        {
            _startTime = DateTime.Now;
            _timer.Elapsed += _timer_Elapsed;
        }

        public string StartDisplayValue => DisplayTime(CountdownSeconds);

        public void Pause()
        {
            _bankedSeconds += Math.Abs(new TimeSpan(_startTime.Ticks).Subtract(new TimeSpan(DateTime.Now.Ticks)).TotalSeconds);
            _startTime = DateTime.Now;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tuple<double, string> value = GetValue();
            TickTock?.Invoke(this, new CountdownEventArgs(value.Item2, value.Item1));

            if (DateTime.Now >= _startTime.AddSeconds(CountdownSeconds))
                End();
        }

        public void End()
        {
            Reset();
            //_bankedSeconds = 0;
            //_startTime = DateTime.MinValue;
            //_timer.Elapsed -= _timer_Elapsed;
            //TimeUp?.Invoke(this, new CountdownEventArgs(StartDisplayValue, CountdownSeconds));
        }

        public void Reset()
        {
            _bankedSeconds = 0;
            _startTime = DateTime.MinValue;
            _timer.Elapsed -= _timer_Elapsed;
            Resetting?.Invoke(this, new CountdownEventArgs(StartDisplayValue, CountdownSeconds));
        }

        public Tuple<double, string> GetValue()
        {
            double totalSecondsElapsed = Math.Abs(new TimeSpan(_startTime.Ticks).Subtract(new TimeSpan(DateTime.Now.Ticks)).TotalSeconds);
            double totalSecondsRemaining = CountdownSeconds - (totalSecondsElapsed + _bankedSeconds);

            return new Tuple<double, string>(totalSecondsRemaining, DisplayTime(totalSecondsRemaining));
        }

        private string DisplayTime(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }
    }
}
