using GalaSoft.MvvmLight;

namespace MvvmLight_Prototypes.Model
{
    public class ExerciseListItem : ObservableObject
    {
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (Set(() => Title, ref title, value))
                    isDirty = true;
            }            
        }

        private int suggestedDuration;
        public int SuggestedDuration
        {
            get { return suggestedDuration; }
            set
            {
                if (Set(() => SuggestedDuration, ref suggestedDuration, value))
                    isDirty = true;
            }
        }

        private int difficultyRating;
        public int DifficultyRating
        {
            get { return difficultyRating; }
            set
            {
                if (Set(() => DifficultyRating, ref difficultyRating, value))
                    isDirty = true;
            }
        }

        private int practicalityRating;
        public int PracticalityRating
        {
            get { return practicalityRating; }
            set
            {
                if (Set(() => PracticalityRating, ref practicalityRating, value))
                    isDirty = true;
            }
        }

        private bool scribed;
        public bool Scribed
        {
            get { return scribed; }
            set
            {
                if (Set(() => Scribed, ref scribed, value))
                    isDirty = true;
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                if (Set(() => Notes, ref notes, value))
                    isDirty = true;
            }
        }
    }
}
