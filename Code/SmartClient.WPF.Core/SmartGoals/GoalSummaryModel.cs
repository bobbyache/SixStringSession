using Caliburn.Micro;

namespace SmartGoals
{
    public class GoalSummaryModel : PropertyChangedBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { 
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}
