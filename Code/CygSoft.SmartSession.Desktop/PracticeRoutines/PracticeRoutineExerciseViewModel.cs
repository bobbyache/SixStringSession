using AutoMapper;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using GalaSoft.MvvmLight;

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

        private int frequencyWeighting;
        public int FrequencyWeighting
        {
            get
            {
                return frequencyWeighting;
            }
            set
            {
                practiceRoutineExercise.FrequencyWeighting = value;
                Set(() => FrequencyWeighting, ref frequencyWeighting, value);
            }
        }

        public PracticeRoutineExerciseViewModel(PracticeRoutineExercise practiceRoutineExercise)
        {
            this.practiceRoutineExercise = practiceRoutineExercise;
            Mapper.Map(practiceRoutineExercise, this);
        }
    }
}
