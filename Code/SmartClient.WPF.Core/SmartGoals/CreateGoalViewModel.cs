using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using System.ComponentModel.DataAnnotations;

namespace SmartGoals
{
	public class CreateGoalViewModel : ValidatableScreen
    {
		
		private string title;
		[Required]
		public string Title
		{
			get { return title; }
			set 
			{ 
				title = value;
				NotifyOfPropertyChange(() => Title);
				Validate(() => Title, value);
				NotifyOfPropertyChange(() => CanSubmit);
				
			}
		}

		public int WeightingPercentage { get; set; } = 50;

		
		private string filePath;
		[Required]
		public string FilePath
		{
			get { return filePath; }
			set 
			{ 
				filePath = value;
				NotifyOfPropertyChange(() => FilePath);
				Validate(() => FilePath, value);
				NotifyOfPropertyChange(() => CanSubmit);
			}
		}



		private readonly GoalManager goalManager;

		public CreateGoalViewModel(IEventAggregator eventAggregator, 
			IDialogService dialogService, 
			ISettingsService settingsService, 
			GoalManager goalManager): base(eventAggregator, dialogService, settingsService)
		{
			this.goalManager = goalManager;
			ValidateAll();
		}

		public void Cancel()
		{
			Notify(new NavigateToMessage(NavigateTo.Home));
		}

		public void Submit()
		{
			this.goalManager.Open(this.FilePath);
			var editableGoal = this.goalManager.GetEditableGoal();
			editableGoal.Title = this.Title;
			editableGoal.Weighting = this.WeightingPercentage / 100d;
			this.goalManager.Save();
			Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
		}

		public bool CanSubmit
		{
			get { return !validator.HasErrors; }
		}
	}
}
