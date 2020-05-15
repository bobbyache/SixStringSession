using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartSession.RecentProjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class IntroViewModel : Screen
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogService dialogService;
        private readonly ISettingsService settingsService;
        private readonly GoalManager goalManager;
        private RecentProjectsRepository recentProjects = new RecentProjectsRepository();

        public IntroViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, GoalManager goalManager)
        {
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;
            this.settingsService = settingsService;
            this.goalManager = goalManager;
        }

        private RecentProject selectedRecentProject;
        public RecentProject SelectedRecentProject
        {
            get { return selectedRecentProject; }
            set
            {
                this.selectedRecentProject = value;
                NotifyOfPropertyChange("SelectedRecentProject");
            }
        }

        public void OpenRecentProject()
        {
            if (SelectedRecentProject != null)
            {
                this.goalManager.Open(SelectedRecentProject.FilePath);
                eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.GoalDashboard));
            }
        }

        public void OpenProject()
        {
            string filePath;
            var opening = this.dialogService.OpenFile(settingsService.InitialProjectDirectory, out filePath);
            if (opening)
            {
                this.goalManager.Open(filePath);
                eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.GoalDashboard));
            }
        }

        public void CreateProject()
        {
            eventAggregator.PublishOnUIThreadAsync(new NavigateToMessage(NavigateTo.CreateGoal));
        }

        public List<RecentProject> RecentProjects
        {
            get
            {
                return recentProjects.Open(@"C:\Code\recent_projects.json");
            }
        }
    }
}
