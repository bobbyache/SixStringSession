using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Controls.GuiTests
{
    // Using the EventArgsConverter in MVVM Light
    // https://galasoft.ch/posts/2014/01/using-the-eventargsconverter-in-mvvm-light-and-why-is-there-no-eventtocommand-in-the-windows-8-1-version

    public class SelectedTimeChangedEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = value as TimeSelectedChangedRoutedEventArgs;
            if (args != null)
            {
                return args.NewTime;
            }
            throw new InvalidOperationException("Could not resolved event args for TimePicker.SelectedTimeChangedEventArgs.");
        }
    }
}
