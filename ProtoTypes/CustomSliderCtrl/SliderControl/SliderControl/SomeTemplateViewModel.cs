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
        public SomeTemplateViewModel()
        {
            // Create command setting Value as Slider's NewValue
            ValueChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<double>>(
                args => CurrentManualProgress = args.NewValue);

            DecreaseManualProgressCommand = new RelayCommand(() => DecreaseManualProgress(), () => true);
            IncreaseManualProgressCommand = new RelayCommand(() => IncreaseManualProgress(), () => true);

            InitialManualProgress = 35;
            CurrentManualProgress = 35;
        }

        private double currentManualProgress;
        public double CurrentManualProgress
        {
            get { return currentManualProgress; }
            set
            {
                currentManualProgress = value;
                RaisePropertyChanged();
                RaisePropertyChanged("ProgressInformationText");
            }
        }

        private double initialManualProgress;
        public double InitialManualProgress
        {
            get { return initialManualProgress; }
            set
            {
                initialManualProgress = value;
                RaisePropertyChanged();
                RaisePropertyChanged("ProgressInformationText");
            }
        }

        public string ProgressInformationText
        {
            get
            {
                bool positive = CurrentManualProgress > InitialManualProgress;
                double difference = Math.Abs(CurrentManualProgress - InitialManualProgress);

                string positiveSign = positive ? "+" : "-";

                if (difference == 0)
                    return $"{CurrentManualProgress}%";
                else
                    return $"{CurrentManualProgress}% ({positiveSign}{difference})";
            }
        }

        public ICommand ValueChangedCommand { get; set; }
        public RelayCommand DecreaseManualProgressCommand { get; private set; }
        public RelayCommand IncreaseManualProgressCommand { get; private set; }

        private void DecreaseManualProgress()
        {
            if (CurrentManualProgress > 0)
                CurrentManualProgress--;
        }

        private void IncreaseManualProgress()
        {
            if (CurrentManualProgress < 100)
            CurrentManualProgress++;
        }
    }
}
