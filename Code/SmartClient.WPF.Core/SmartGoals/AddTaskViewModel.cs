using Caliburn.Micro;
using SmartClient.Domain;
using SmartClient.Domain.Common;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class AddTaskViewModel : ValidatableScreen, IHandle<AddTaskMessage>
    {
        private IGoalDetail goal;
        private IEditableGoalTask goalTask;
        private readonly GoalManager goalManager;

        public string GoalTitle
        {
            get { return this.goalTask.GoalTitle; }
        }

        [Required]
        [StringLength(250, MinimumLength=3, ErrorMessage="Title must be between 3 and 250 characters")]
        public string Title
        {
            get { return this.goalTask.Title; }
            set 
            { 
                this.goalTask.Title = value;
                SetAndValidate(() => Title, value);
            }
        }

        public double Start
        {
            get { return this.goalTask.Start; }
            set 
            { 
                this.goalTask.Start = value;
                SetAndValidate(() => Start, value);
            }
        }

        public double Target
        {
            get { return this.goalTask.Target; }
            set 
            { 
                this.goalTask.Target = value;
                SetAndValidate(() => Target, value);
            }
        }

        public int WeightingPercentage
        {
            get { return (int)(this.goalTask.Weighting * 100); }
            set 
            { 
                this.goalTask.Weighting = (double)value / 100;
                SetAndValidate(() => Target, value);
            }
        }

        public TaskUnitOfMeasure UnitOfMeasure
        {
            get { return this.goalTask.UnitOfMeasure; }
            set
            {
                this.goalTask.UnitOfMeasure = value;
                SetAndValidate(() => UnitOfMeasure, value);
            }
        }

        public AddTaskViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService,
            GoalManager goalManager)
            : base(eventAggregator, dialogService, settingsService)
        {
            this.goalManager = goalManager;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        public Task HandleAsync(AddTaskMessage message, CancellationToken cancellationToken)
        {
            this.goal = message.Goal;
            this.goalTask = goalManager.CreateTask();

            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => Target);
            NotifyOfPropertyChange(() => Start);
            NotifyOfPropertyChange(() => WeightingPercentage);
            NotifyOfPropertyChange(() => UnitOfMeasure);

            return Task.CompletedTask;
        }
        public void Cancel()
        {
            Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
        }

        public void Submit()
        {
            this.goalManager.AddTask(this.goalTask);
            this.goalManager.Save();
            Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
        }

    }
}
