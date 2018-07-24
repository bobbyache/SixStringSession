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
        private System.Timers.Timer aTimer = new System.Timers.Timer();
        private Countdown countDown;

        public Form1()
        {
            InitializeComponent();
            countDown = new Countdown(aTimer, 5);
            lblCurrentTaskTimeLeft.Text = countDown.StartDisplayValue;
            btnStartPause.Text = "Start";
            aTimer.Interval = 500;
        }

        private void btnStartPause_Click(object sender, EventArgs e)
        {
            if (btnStartPause.Text == "Start")
            {
                btnStartPause.Text = "Pause";
                aTimer.Enabled = true;
                
                countDown.Resetting += CountDown_Resetting;
                countDown.TickTock += CountDown_TickTock;
                countDown.Start();
            }
            else
            {
                btnStartPause.Text = "Start";
                aTimer.Enabled = false;
                countDown.Pause();
                countDown.TickTock -= CountDown_TickTock;
            }
        }

        private void CountDown_Resetting(object sender, CountdownEventArgs e)
        {
            btnStartPause.InvokeIfRequired(b => b.Text = "Start");
            lblCurrentTaskTimeLeft.InvokeIfRequired(c => c.Text = e.DisplayString);
            countDown.Resetting -= CountDown_Resetting;
        }

        private void CountDown_TickTock(object sender, CountdownEventArgs e)
        {
            lblCurrentTaskTimeLeft.InvokeIfRequired(c => c.Text = e.DisplayString);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            countDown.Reset();
            btnStartPause.Text = "Start";
        }
    }
}
