using Caliburn.Micro;
using JsonDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SmartGoals
{
    public class ExampleViewModel: Screen
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

        public string GreetingsMessage
        {
            get { return this.greetingMessageProvider.Get(); }
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

        public HeaderViewModel HeaderViewModel { get; }
        public ContentViewModel ContentViewModel { get; }

        private GoalRepository repository = new GoalRepository(@"C:\Users\RobB\OneDrive - Korbitec Inc\Documents\Guitar\Goals");

        private readonly GreetingsMessageProvider greetingMessageProvider;

        public ExampleViewModel(HeaderViewModel headerViewModel, ContentViewModel contentViewModel, GreetingsMessageProvider greetingMessageProvider)
        {
            HeaderViewModel = headerViewModel;
            ContentViewModel = contentViewModel;
            this.greetingMessageProvider = greetingMessageProvider;

            var goalItems = repository.GetGoalList("goals.json");
            foreach (var goal in goalItems)
            {
                GoalSummaryModel model = new GoalSummaryModel(goal);
                Goals.Add(model);
            }
        }

        public void SayHello()
        {
            // Binds by naming convention
            MessageBox.Show(string.Format("Hello {0}!", Name)); //Don't do this in real life :)
        }

        public void DoubleClicked()
        {
            // F1 on button to go to help page for event types that can be actioned. See weird syntax in xaml
            MessageBox.Show(string.Format("Yeeha {0}, I've been double clicked!", Name)); //Don't do this in real life :)
        }

        public void SayHelloClickedOnDblClickWithParam(EventArgs eventArgs)
        {
            /*
             * $eventArgs - Event Arguments
             * $dataContext = the view model
             * $source = the framework element that triggered the action
             * $view = the whole user control
             * $this = the ui element to which the action is attached
             * */
            Console.WriteLine(eventArgs);
        }


        public void LabelClicked()
        {
            Console.WriteLine("yeah clicked the label");
        }
    }
}
