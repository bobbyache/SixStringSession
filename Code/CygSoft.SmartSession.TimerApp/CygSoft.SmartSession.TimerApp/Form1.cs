using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace CygSoft.SmartSession.TimerApp
{
    public partial class Form1 : Form
    {
        private DateTime _startTime = DateTime.MinValue;
        private System.Timers.Timer aTimer = new System.Timers.Timer();
        private Countdown countDown = new Countdown(65);

        public Form1()
        {
            InitializeComponent();
            btnStartPause.Text = "Start";
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 500;
        }

        private void btnStartPause_Click(object sender, EventArgs e)
        {
            
            if (_startTime == DateTime.MinValue)
                _startTime = DateTime.Now;
            aTimer.Enabled = !aTimer.Enabled;

            btnStartPause.Text = btnStartPause.Text == "Start" ? "Pause" : "Start";
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lblCurrentTaskTimeLeft.InvokeIfRequired(c => c.Text = countDown.GetValue(_startTime, DateTime.Now));

            if (DateTime.Now >= _startTime.AddSeconds(countDown.CountdownSeconds))
                Reset();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            aTimer.Enabled = false;
            _startTime = DateTime.MinValue;
            btnStartPause.InvokeIfRequired(c => c.Text = "Start");
        }
    }
}
