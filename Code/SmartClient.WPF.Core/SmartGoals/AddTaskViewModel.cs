using Caliburn.Micro;
using SmartClient.Domain;
using SmartClient.Domain.Common;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGoals
{
    public class AddTaskViewModel : ValidatableScreen
    {
        private IEditableGoalTask goalTask;
        private readonly GoalManager goalManager;

        public string GoalTitle
        {
            get { return this.goalTask.GoalTitle; }
        }

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
                // this.goalTask.UnitOfMeasure = TaskUnitOfMeasure.BPM;
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
            this.goalTask = goalManager.CreateTask();

            NotifyOfPropertyChange(() => Title);
            NotifyOfPropertyChange(() => Target);
            NotifyOfPropertyChange(() => Start);

            return base.OnActivateAsync(cancellationToken);
        }
    }
}
