using Caliburn.Micro;
using JsonDb;
using System.Windows;

namespace SmartGoals
{
    public class ShellViewModel : Screen
    {
        string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        private BindableCollection<GoalSummaryModel> goals = new BindableCollection<GoalSummaryModel>();

        public BindableCollection<GoalSummaryModel> Goals
        {
            get { return goals; }
            set { goals = value; }
        }

        private GoalSummaryModel selectedGoalSummary;

        public GoalSummaryModel SelectedGoalSummary
        {
            get { return selectedGoalSummary; }
            set 
            {
                selectedGoalSummary = value;
                NotifyOfPropertyChange(() => SelectedGoalSummary);
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        private GoalRepository repository = new GoalRepository(@"C:\Users\RobB\OneDrive - Korbitec Inc\Documents\Guitar\Goals");

        public ShellViewModel()
        {
            var goalItems = repository.GetGoalList("goals.json");
            foreach (var goal in goalItems)
            {
                GoalSummaryModel model = new GoalSummaryModel(goal);
                Goals.Add(model);
            }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }
    }
}