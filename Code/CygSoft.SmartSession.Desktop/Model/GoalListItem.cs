using GalaSoft.MvvmLight;

namespace MvvmLight_Prototypes.Model
{
    public class GoalListItem : ObservableObject
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(() => Title, ref title, value); }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set { Set(() => Progress, ref progress, value); }
        }

        private int days;
        public int Days
        {
            get { return days; }
            set { Set(() => Days, ref days, value); }
        }

        private bool onSchedule;
        public bool OnSchedule
        {
            get { return onSchedule; }
            set { Set(() => OnSchedule, ref onSchedule, value); }
        }
    }
}
