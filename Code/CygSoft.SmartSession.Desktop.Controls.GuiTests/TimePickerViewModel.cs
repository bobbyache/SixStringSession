using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Controls.GuiTests
{
    public class TimePickerViewModel : ObservableObject
    {
        private TimeSpan time = new TimeSpan();

        public TimeSpan TotalTime
        {
            get { return time; }
            set
            {
                Set("TotalTime", ref time, value);
            }
        }

        public TimePickerViewModel()
        {
            time = new TimeSpan(10, 10, 10);
        }
    }
}
