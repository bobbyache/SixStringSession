using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Navigation;

namespace SmartGoals
{
	public class CreateGoalViewModel : BaseScreen
    {
		[Required]
		public string Title { get; set; } = "";
		public int WeightingPercentage { get; set; } = 50;
		public string FilePath { get; set; }

		private readonly GoalManager goalManager;

		public CreateGoalViewModel(IEventAggregator eventAggregator, 
			IDialogService dialogService, 
			ISettingsService settingsService, 
			GoalManager goalManager): base(eventAggregator, dialogService, settingsService)
		{
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
