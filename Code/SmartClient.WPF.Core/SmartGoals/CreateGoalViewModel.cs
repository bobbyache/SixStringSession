using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Navigation;

namespace SmartGoals
{
	public class CreateGoalViewModel : Screen
    {
		private int weightingPercentage = 50;
		public int WeightingPercentage
		{
			get 
			{
				return weightingPercentage;
				// return (int)(0.95 * 100);
			}
			set 
			{ 
				weightingPercentage = value; 
			}
		}

		private string title = "";
		[Required]
		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		private string filePath;
		private readonly IEventAggregator eventAggregator;
		private readonly IDialogService dialogService;
		private readonly ISettingsService settingsService;
		private readonly GoalManager goalManager;

		public string FilePath
		{
			// get { return @"C:\Users\Robb\Code\example.json"; }
			get { return this.filePath; }
			set { this.filePath = value; }
		}

		public CreateGoalViewModel(IEventAggregator eventAggregator, 
			IDialogService dialogService, 
			ISettingsService settingsService, 
			GoalManager goalManager)
		{
			this.eventAggregator = eventAggregator;
			this.dialogService = dialogService;
			this.settingsService = settingsService;
			this.goalManager = goalManager;
		}

		public void Cancel()
		{
			eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.Home));
		}

		public void Submit()
		{
			this.goalManager.Open(this.FilePath);
			var editableGoal = this.goalManager.GetEditableGoal();
			editableGoal.Title = this.Title;
			editableGoal.Weighting = this.WeightingPercentage / 100d;
			this.goalManager.Save();
			eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.GoalDashboard));
		}
	}
}
