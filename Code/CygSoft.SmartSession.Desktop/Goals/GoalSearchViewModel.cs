using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Goals;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Goals
{
    public class GoalSearchViewModel : ViewModelBase
    {

        #region Alternative Constructors
        //public GoalSearchViewModel(IGoalService goalService, IDialogViewService dialogService, INavigationService navigationService)
        //{
        //    this.goalService = goalService;
        //    this.dialogService = dialogService;
        //    this.navigationService = navigationService;
        //}

        // for blend:
        //public GoalSearchViewModel() : this(new GoalService(), new DialogService(), new NavigationService())
        //{
        //    ...
        //}

        #endregion

        private IGoalService goalService;
        private IDialogViewService dialogService;

        public GoalSearchViewModel(GoalSearchCriteriaViewModel goalSearchCriteriaViewModel, IGoalService goalService, IDialogViewService dialogService)
        {
            this.goalSearchCriteriaViewModel = goalSearchCriteriaViewModel ?? throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.goalService = goalService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddGoalCommand = new RelayCommand(AddGoal, () => true);
            DeleteGoalCommand = new RelayCommand(DeleteGoal, () => SelectedGoal != null);
            EditGoalCommand = new RelayCommand(EditGoal, () => SelectedGoal != null);

            Messenger.Default.Register<FindGoalsMessage>(this, Find);
        }

        public RelayCommand AddGoalCommand { get; private set; }
        public RelayCommand DeleteGoalCommand { get; private set; }
        public RelayCommand EditGoalCommand { get; private set; }

        private GoalSearchResult selectedGoal;
        public GoalSearchResult SelectedGoal
        {
            get { return selectedGoal; }
            set
            {
                Set(() => SelectedGoal, ref selectedGoal, value);
                AddGoalCommand.RaiseCanExecuteChanged();
                EditGoalCommand.RaiseCanExecuteChanged();
                DeleteGoalCommand.RaiseCanExecuteChanged();
            }
        }

        private GoalSearchCriteriaViewModel goalSearchCriteriaViewModel;
        public GoalSearchCriteriaViewModel GoalSearchCriteriaViewModel
        {
            get { return goalSearchCriteriaViewModel; }
            set { Set(() => GoalSearchCriteriaViewModel, ref goalSearchCriteriaViewModel, value); }
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
        public ObservableCollection<GoalSearchResult> GoalList { get; private set; } = new ObservableCollection<GoalSearchResult>();


        private void Find(FindGoalsMessage obj)
        {
            GoalList.Clear();

            var searchCriteria = Mapper.Map<GoalSearchCriteriaViewModel, GoalSearchCriteria>(GoalSearchCriteriaViewModel);

            foreach (var goal in goalService.Find(searchCriteria))
            {
                GoalList.Add(Mapper.Map<GoalSearchResult>(goal));
            }
        }

        private void EditGoal()
        {
            Messenger.Default.Send(new StartEditingGoalMessage(SelectedGoal));
            //dialogService.ShowMessage($"Edited - {DateTime.Now}. This is an extra little note.", "Edit");
        }

        private void DeleteGoal()
        {
            goalService.Remove(SelectedGoal.Id);
            GoalList.Remove(SelectedGoal);
        }

        private void AddGoal()
        {
            var goal = new GoalSearchResult
            {
                Title = $"New Goal Item - {DateTime.Now}",
                Notes = null
            };

            var domainGoal = Mapper.Map<Goal>(goal);
            goalService.Add(domainGoal);
            Mapper.Map(domainGoal, goal);

            GoalList.Add(goal);
            SelectedGoal = goal;
        }
    }
}