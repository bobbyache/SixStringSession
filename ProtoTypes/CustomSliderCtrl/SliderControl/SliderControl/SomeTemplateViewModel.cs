using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SliderControl
{
    public class SomeTemplateViewModel : ViewModelBase
    {
        private double _value;

        public SomeTemplateViewModel()
        {
            // Create command setting Value as Slider's NewValue
            ValueChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<double>>(
                args => Value = args.NewValue);

            Value = 35;
        }

        public ICommand ValueChangedCommand { get; set; }

        public double Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); } // Notify UI
        }
    }
}
