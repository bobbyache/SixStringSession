using GalaSoft.MvvmLight;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSearchResultModel : ObservableObject
    {
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        public int Id { get; set; }

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
    }
}
