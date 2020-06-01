using Caliburn.Micro;
using SmartClient.Domain;
using SmartGoals.Services;
using SmartGoals.Supports.CommonScreens;
using SmartSession.RecentProjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals
{
    public class IntroViewModel : BaseScreen
    {
        private readonly GoalManager goalManager;
        private RecentProjectsRepository recentProjects = new RecentProjectsRepository();

        public IntroViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService, GoalManager goalManager)
            : base(eventAggregator, dialogService, settingsService)
        {
            this.goalManager = goalManager;
        }

        private RecentProject selectedRecentProject;
        public RecentProject SelectedRecentProject
        {
            get { return selectedRecentProject; }
            set
            {
                this.selectedRecentProject = value;
                NotifyOfPropertyChange(() => SelectedRecentProject);
            }
        }

        public void OpenRecentProject()
        {
            if (SelectedRecentProject != null)
            {
                this.goalManager.Open(SelectedRecentProject.FilePath);
                Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
            }
        }

        public void OpenProject()
        {
            string filePath;
            var opening = Dialogs.OpenFile(Settings.InitialProjectDirectory, out filePath);
            if (opening)
            {
                this.goalManager.Open(filePath);
                Notify(new NavigateToMessage(NavigateTo.GoalDashboard));
            }
        }

        public void CreateProject()
        {
            Notify(new NavigateToMessage(NavigateTo.CreateGoal));
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
