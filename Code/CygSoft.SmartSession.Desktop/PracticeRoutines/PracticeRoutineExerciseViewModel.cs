using AutoMapper;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: You need to be able to work out the amount of time you've currently allocated to your Practice Routine, and your BindingList will need this class.

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineExerciseViewModel : ViewModelBase
    {
        private PracticeRoutineExercise practiceRoutineExercise;

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
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
                practiceRoutineExercise.Minutes = value;
                Set(() => Minutes, ref minutes, value);
            }
        }

        public PracticeRoutineExerciseViewModel(PracticeRoutineExercise practiceRoutineExercise)
        {
            this.practiceRoutineExercise = practiceRoutineExercise;
            Mapper.Map(practiceRoutineExercise, this);
        }
    }
}
