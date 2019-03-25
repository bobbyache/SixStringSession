using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using WPFNotification.Core.Configuration;
using WPFNotification.Model;
using WPFNotification.Services;

namespace Example_1
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Recorder recorder = new Recorder();

        public string DisplayTime { get { return recorder.DisplayTime; } }

        public double Progress { get { return CurrentProgress(); } }

        public RelayCommand ResumeCommand { get; private set; }
        public RelayCommand PauseCommand { get; private set; }

        public RelayCommand BackSmallCommand { get; private set; }

        public RelayCommand BackLargeCommand { get; private set; }

        public RelayCommand ForwardLargeCommand { get; private set; }

        public RelayCommand ForwardSmallCommand { get; private set; }


        private TaskbarItemProgressState progressState;
        public TaskbarItemProgressState ProgressState
        {
            get
            {
                return progressState;
            }
            set
            {
                Set(() => ProgressState, ref progressState, value);
            }
        }

        private double CurrentProgress() => recorder.PreciseSeconds / 300;

        public MainWindowViewModel()
        {
            recorder = new Recorder();
            recorder.RecordingStatusChanged += Recorder_RecordingStatusChanged;
            recorder.Tick += Recorder_Tick;
            
            ResumeCommand = new RelayCommand(() => Resume(), () => true);
            PauseCommand = new RelayCommand(() => Pause(), () => true);
            BackSmallCommand = new RelayCommand(() => BackSmall(), () => true);
            BackLargeCommand = new RelayCommand(() => BackLarge(), () => true);
            ForwardLargeCommand = new RelayCommand(() => ForwardLarge(), () => true);
            ForwardSmallCommand = new RelayCommand(() => ForwardSmall(), () => true);
        }

        private bool beenNotifiedTimeExceeded = false;

        private void Recorder_Tick(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => DisplayTime);
            RaisePropertyChanged(() => Progress);

            QueryNotification();
            ChangeProgressStatus();

            RaisePropertyChanged(() => ProgressState);
        }

        private void ChangeProgressStatus()
        {
            if (!recorder.Recording)
                ProgressState = TaskbarItemProgressState.Paused;

            if (recorder.Recording && !FullTimeAchieved())
                ProgressState = TaskbarItemProgressState.Normal;

            if (recorder.Recording && FullTimeAchieved())
                ProgressState = TaskbarItemProgressState.Indeterminate;
        }

        private void QueryNotification()
        {
            var timeAchieved = FullTimeAchieved();

            if (timeAchieved && !beenNotifiedTimeExceeded)
            {
                beenNotifiedTimeExceeded = true;
                Notify();
            }
            else if (timeAchieved) { /* do nothing */ }
            else
            {
                beenNotifiedTimeExceeded = false;
            }
        }

        private bool FullTimeAchieved() => CurrentProgress() >= 1;

        private INotificationDialogService _dialogService = new NotificationDialogService();

        private void Notify()
        {
            // var notifcationConfig = new NotificationConfiguration()
            var notificationConfiguration = NotificationConfiguration.DefaultConfiguration;
            notificationConfiguration.NotificationFlowDirection = NotificationFlowDirection.RightBottom;
            var newNotification = new Notification()
            {
                Title = "Test Fail",
                Message = "Test one Fail Please check your Machine Code and Try Again"
                // ,ImgURL = "pack://application:,,,a/Resources/Images/warning.png"
            };

            Application.Current.Dispatcher.Invoke((Action)delegate {
                _dialogService.ShowNotificationWindow(newNotification, notificationConfiguration);
            });
            
        }

        private void Recorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            ChangeProgressStatus();
        }

        private void Resume()
        {
            recorder.Resume();
        }

        private void Pause()
        {
            recorder.Pause();
        }

        private void BackSmall()
        {
            recorder.Pause();
            recorder.SubstractSeconds(5);
            recorder.Resume();
        }

        private void ForwardSmall()
        {
            recorder.Pause();
            recorder.AddSeconds(60);
            recorder.Resume();
        }

        private void ForwardLarge()
        {
            recorder.Pause();
            recorder.SubstractSeconds(5);
            recorder.Resume();
        }

        private void BackLarge()
        {
            recorder.Pause();
            recorder.SubstractSeconds(60);
            recorder.Resume();
        }


    }
}
