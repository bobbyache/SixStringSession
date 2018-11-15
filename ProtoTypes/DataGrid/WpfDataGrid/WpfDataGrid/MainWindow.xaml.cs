using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //List<User> users = new List<User>();
            //users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            //users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            //users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

            ObservableCollection<ExerciseActivity> activities = new ObservableCollection<ExerciseActivity>();
            activities.Add(new ExerciseActivity()
            {
                DateCreated = DateTime.Parse("2018-01-02 12:31:56"),
                DateModified = DateTime.Parse("2018-01-02 12:31:56"),
                StartTime = DateTime.Parse("2018-01-02 12:31:56"),
                EndTime = DateTime.Parse("2018-01-02 12:31:56"),
                TotalSeconds = 500,
                MetronomeSpeed = 80
            });

            dgUsers.ItemsSource = activities;
        }

        public abstract class Entity
        {
            public int Id { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
        }

        public class ExerciseActivity : Entity
        {
            public int MetronomeSpeed { get; set; }

            private TimeSpan Time { get; set; } = new TimeSpan();
            public int TotalSeconds
            {
                get { return (int)Time.TotalSeconds; }
                set { Time = TimeSpan.FromSeconds(value); }
            }
            
            public int Hours
            {
                get
                {
                    return Time.Hours;
                }
            }

            public int Minutes
            {
                get
                {
                    return Time.Minutes;
                }
            }

            public int Seconds
            {
                get
                {
                    return Time.Seconds;
                }
            }

            // You still need these to work out your metronome calculations.
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }

            public int ExerciseId { get; set; }

        }

        //public class User
        //{
        //    public int Id { get; set; }

        //    public string Name { get; set; }

        //    public DateTime Birthday { get; set; }
        //}
    }
}
