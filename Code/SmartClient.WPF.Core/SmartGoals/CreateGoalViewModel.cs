using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using SmartGoals.Supports.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SmartGoals
{
	public class CreateGoalViewModel : ValidatableScreen
    {
		
		private string title;
		[Required]
		[StringLength(500, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 500 characters")]
		public string Title
		{
			get { return title; }
			set 
			{ 
				title = value;
				SetAndValidate(() => Title, value);
			}
		}

		public int WeightingPercentage { get; set; } = 50;

		
		private string filePath;
		[Required]
		[ValidFilePathValidator]
		public string FilePath
		{
			get { return filePath; }
			set 
			{ 
				filePath = value;
				SetAndValidate(() => FilePath, value);
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

		public void CreateFilePath()
		{
			string filePath;
			var opened = Dialogs.CreateFile(Settings.InitialProjectDirectory, ".json", "JSON Files (*.json)|*.json", out filePath);
			if (opened)
			{
				FilePath = filePath;

				if (string.IsNullOrEmpty(Title))
				{
					Title = Path.GetFileNameWithoutExtension(FilePath);
				}
			}
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
	}
}
