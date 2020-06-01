using Caliburn.Micro;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System;

namespace SmartGoals
{
    public class ExampleViewModel: BaseScreen
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

        public BindableCollection<GoalSummaryModel> Goals { get; set; } = new BindableCollection<GoalSummaryModel>();

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

        public ExampleViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, 
            HeaderViewModel headerViewModel, ContentViewModel contentViewModel) 
            : base(eventAggregator, dialogService, settingsService)
        {
            HeaderViewModel = headerViewModel;
            ContentViewModel = contentViewModel;
        }

        public void SayHello()
        {
            Dialogs.ExclamationMessage("Hello message", string.Format("Hello {0}!", Name));
        }

        public void DoubleClicked()
        {
            Dialogs.ExclamationMessage("Click Message", string.Format("Yeeha {0}, I've been double clicked!", Name));
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
