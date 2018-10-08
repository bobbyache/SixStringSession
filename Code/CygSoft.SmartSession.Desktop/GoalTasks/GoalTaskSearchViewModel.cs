using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.GoalTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    public class GoalTaskSearchViewModel : ViewModelBase
    {

        #region Alternative Constructors
        //public GoalTaskSearchViewModel(IGoalTaskService goalTaskService, IDialogViewService dialogService, INavigationService navigationService)
        //{
        //    this.goalTaskService = goalTaskService;
        //    this.dialogService = dialogService;
        //    this.navigationService = navigationService;
        //}

        // for blend:
        //public GoalTaskSearchViewModel() : this(new GoalTaskService(), new DialogService(), new NavigationService())
        //{
        //    ...
        //}

        #endregion

        private IGoalTaskService goalTaskService;
        private IDialogViewService dialogService;

        public GoalTaskSearchViewModel(GoalTaskSearchCriteriaViewModel goalTaskSearchCriteriaViewModel, IGoalTaskService goalTaskService, IDialogViewService dialogService)
        {
            this.goalTaskSearchCriteriaViewModel = goalTaskSearchCriteriaViewModel ?? throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.goalTaskService = goalTaskService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddGoalTaskCommand = new RelayCommand(AddGoalTask, () => true);
            DeleteGoalTaskCommand = new RelayCommand(DeleteGoalTask, () => SelectedGoalTask != null);
            EditGoalTaskCommand = new RelayCommand(EditGoalTask, () => SelectedGoalTask != null);

            Messenger.Default.Register<FindGoalTasksMessage>(this, Find);
        }

        public RelayCommand AddGoalTaskCommand { get; private set; }
        public RelayCommand DeleteGoalTaskCommand { get; private set; }
        public RelayCommand EditGoalTaskCommand { get; private set; }

        private GoalTaskSearchResultModel selectedGoalTask;
        public GoalTaskSearchResultModel SelectedGoalTask
        {
            get { return selectedGoalTask; }
            set
            {
                Set(() => SelectedGoalTask, ref selectedGoalTask, value);
                AddGoalTaskCommand.RaiseCanExecuteChanged();
                EditGoalTaskCommand.RaiseCanExecuteChanged();
                DeleteGoalTaskCommand.RaiseCanExecuteChanged();
            }
        }

        private GoalTaskSearchCriteriaViewModel goalTaskSearchCriteriaViewModel;
        public GoalTaskSearchCriteriaViewModel GoalTaskSearchCriteriaViewModel
        {
            get { return goalTaskSearchCriteriaViewModel; }
            set { Set(() => GoalTaskSearchCriteriaViewModel, ref goalTaskSearchCriteriaViewModel, value); }
        }

        private bool isItemsControlOpen;
        public bool IsItemsControlOpen
        {
            get
            {
                return isItemsControlOpen;
            }
            set
            {
                Set(() => IsItemsControlOpen, ref isItemsControlOpen, value);
            }
        }


        public ObservableCollection<int> DifficultyList { get; private set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        public ObservableCollection<int> PracticalityList { get; private set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        public ObservableCollection<GoalTaskSearchResultModel> GoalTaskList { get; private set; } = new ObservableCollection<GoalTaskSearchResultModel>();


        private void Find(FindGoalTasksMessage obj)
        {
            GoalTaskList.Clear();

            var searchCriteria = Mapper.Map<GoalTaskSearchCriteriaViewModel, GoalTaskSearchCriteria>(GoalTaskSearchCriteriaViewModel);

            foreach (var goalTask in goalTaskService.Find(searchCriteria))
            {
                GoalTaskList.Add(Mapper.Map<GoalTaskSearchResultModel>(goalTask));
            }
        }

        private void EditGoalTask()
        {
            Messenger.Default.Send(new StartEditingGoalTaskMessage(SelectedGoalTask));
            //dialogService.ShowMessage($"Edited - {DateTime.Now}. This is an extra little note.", "Edit");
        }

        private void DeleteGoalTask()
        {
            goalTaskService.Remove(SelectedGoalTask.Id);
            GoalTaskList.Remove(SelectedGoalTask);
        }

        private void AddGoalTask()
        {
            var goalTask = new GoalTaskSearchResultModel
            {
                Title = $"New GoalTask Item - {DateTime.Now}",
                Notes = null
            };

            var domainGoalTask = Mapper.Map<GoalTask>(goalTask);
            goalTaskService.Add(domainGoalTask);
            Mapper.Map(domainGoalTask, goalTask);

            GoalTaskList.Add(goalTask);
            SelectedGoalTask = goalTask;
        }
    }
}