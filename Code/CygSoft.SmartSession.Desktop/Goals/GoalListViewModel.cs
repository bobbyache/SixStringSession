using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace CygSoft.SmartSession.Desktop.Goals
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GoalListViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public GoalListViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public IEnumerable<GoalListItem> GoalList
        {
            get => new List<GoalListItem>()
            {
                new GoalListItem { Title = "Learn to play AC/DC's Thunderstruck", Progress = 22, Days = 25, OnSchedule = true },
                new GoalListItem { Title = "Learn to play Yellow Submarine", Progress = 65, Days = 39, OnSchedule = false  },
                new GoalListItem { Title = "Learn the 5 positions of the A minor pentatonic scale", Progress = 94, Days = 12, OnSchedule = true  },
                new GoalListItem { Title = "Learn the barre chords on the 5th string", Progress = 12, Days = 8, OnSchedule = true  },
            };
        }
    }

    
}