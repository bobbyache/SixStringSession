using CygSoft.SmartSession.Desktop.TreeList.Tree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CygSoft.SmartSession.Desktop.TreeList.Demo
{
    public class PracticeRoutineModel : ITreeModel
    {
        public IProjNode SelectedItem { get; set; }

        public IEnumerable GetChildren(object parent)
        {
            if (parent == null)
            {
                return GetTimeSlots();

            }
            else if (parent is TimeSlot)
            {
                return ((TimeSlot)parent).Exercises;
            }
            else
                return new List<object>();
        }

        public bool HasChildren(object parent)
        {
            if (parent is TimeSlot && ((TimeSlot)parent).Exercises != null)
                return ((TimeSlot)parent).Exercises.Count() > 0;
            return false;
        }

        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return new List<TimeSlot>
            {
                new TimeSlot()
                {
                    Title = "Scales (Pentatonic)",
                    Exercises = new List<Exercise>
                    {
                        new Exercise() { Title = "A Minor Pentatonic - E Shape", Progress = "80%", TotalTime = "00:23:32" },
                        new Exercise() { Title = "A Minor Pentatonic - D Shape", Progress = "32%", TotalTime = "00:18:05" },
                        new Exercise() { Title = "A Minor Pentatonic - C Shape", Progress = "15%", TotalTime = "00:02:12" },
                        new Exercise() { Title = "A Minor Pentatonic - A Shape", Progress = "56%", TotalTime = "00:31:11" }
                    }
                },
                new TimeSlot()
                {
                    Title = "Guitar Fretboard Knowlege",
                    Exercises = new List<Exercise>
                    {
                        new Exercise() { Title = "All the notes on the 6th String", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "All the notes on the 5th String", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "All the notes on the 5th Fret", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "All the notes on the 9th Fret", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using the Caged C Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using the Caged A Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using the Caged G Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using the Caged E Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using the Caged D Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using Octave Pattern", Progress = "0%", TotalTime = "00:00:00" },
                        new Exercise() { Title = "Identify Notes using Reverse Octave Pattern", Progress = "0%", TotalTime = "00:00:00" },
                    }
                },
                new TimeSlot()
                {
                    Title = "Guitar Music Theory"
                },
                new TimeSlot()
                {
                    Title = "Lead Guitar"
                },
                new TimeSlot()
                {
                    Title = "Rhythm Guitar"
                },
            };
        }
    }

    public interface IProjNode
    {
        string Title { get; set; }
    }
    public class TimeSlot : IProjNode
    {
        public string Title { get; set; }
        public IEnumerable<Exercise> Exercises { get; set; }
    }

    public class Exercise : IProjNode
    {
        public string Title { get; set; }
        public string Progress { get; set; }
        public string TotalTime { get; set; }
    }
}
