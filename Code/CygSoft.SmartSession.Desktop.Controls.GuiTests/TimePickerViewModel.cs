using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Controls.GuiTests
{
    // WPF usercontrol Twoway binding Dependency Property
    // https://stackoverflow.com/questions/25989018/wpf-usercontrol-twoway-binding-dependency-property

    public class TimePickerViewModel : ObservableObject
    {
        private TimeSpan time = new TimeSpan();
        ObservableCollection<ExerciseActivity> activities = new ObservableCollection<ExerciseActivity>();

        public TimeSpan TotalTime
        {
            get { return time; }
            set
            {
                Set("TotalTime", ref time, value);
            }
        }

        public ObservableCollection<ExerciseActivity> Activities
        {
            get { return activities; }
            set
            {
                Set("Activities", ref activities, value);
            }
        }

        public TimePickerViewModel()
        {
            time = new TimeSpan(10, 10, 10);
            ChangeTimeCommand = new RelayCommand<object>(ChangeTime);

            activities.Add(new ExerciseActivity()
            {
                DateCreated = DateTime.Parse("2018-01-02 12:31:56"),
                DateModified = DateTime.Parse("2018-01-02 12:31:56"),
                StartTime = DateTime.Parse("2018-01-02 12:31:56"),
                EndTime = DateTime.Parse("2018-01-02 12:31:56"),
                Hours = 4,
                Minutes = 32,
                Seconds = 10,
                //PracticeTime = new TimeSpan(11, 11, 11),
                MetronomeSpeed = 80
            });
        }

        private void ChangeTime(object currentTime)
        {
            TotalTime = (TimeSpan)currentTime;
        }

        public RelayCommand<object> ChangeTimeCommand { get; private set; }

        public abstract class Entity : ObservableObject
        {
            public int Id { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
        }

        public class ExerciseActivity : Entity
        {
            private int metronomeSpeed;
            public int MetronomeSpeed
            {
                get
                {
                    return metronomeSpeed;
                }
                set
                {
                    Set(() => MetronomeSpeed, ref metronomeSpeed, value);
                }
            }

            //private TimeSpan time;
            //public TimeSpan PracticeTime
            //{
            //    get
            //    {
            //        return time;
            //    }
            //    set
            //    {
            //        Set(() => PracticeTime, ref time, value);
            //    }
            //}

            // You still need these to work out your metronome calculations.
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }

            public int ExerciseId { get; set; }
            private int hours;
            public int Hours
            {
                get
                {
                    return hours;
                }
                set
                {
                    Set(() => Hours, ref hours, value);
                }
            }
            private int minutes;
            public int Minutes
            {
                get
                {
                    return minutes;
                }
                set
                {
                    Set(() => Minutes, ref minutes, value);
                }
            }
            private int seconds;
            public int Seconds
            {
                get
                {
                    return seconds;
                }
                set
                {
                    Set(() => Seconds, ref seconds, value);
                }
            }
        }
    }
}
