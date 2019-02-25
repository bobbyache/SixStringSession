using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public abstract class ExerciseSearchViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public ExerciseSearchViewModel(ExerciseSearchCriteriaViewModel exerciseSearchCriteriaViewModel, IExerciseService exerciseService, 
            IDialogViewService dialogService)
        {
            this.exerciseSearchCriteriaViewModel = exerciseSearchCriteriaViewModel ?? 
                throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddExerciseCommand = new RelayCommand(AddExercise, () => true);
            DeleteExerciseCommand = new RelayCommand(DeleteExercise, () => SelectedExercise != null);
            EditExerciseCommand = new RelayCommand(EditExercise, () => SelectedExercise != null);
            SelectExerciseCommand = new RelayCommand(SelectExercise, () => SelectedExercise != null);

            Messenger.Default.Register<FindExercisesMessage>(this, Find);
        }

        protected virtual void SelectExercise() { }

        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteExerciseCommand { get; private set; }
        public RelayCommand EditExerciseCommand { get; private set; }

        public RelayCommand SelectExerciseCommand { get; private set; }

        private ExerciseSearchResultModel selectedExercise;
        public ExerciseSearchResultModel SelectedExercise
        {
            get { return selectedExercise; }
            set
            {
                Set(() => SelectedExercise, ref selectedExercise, value);
                AddExerciseCommand.RaiseCanExecuteChanged();
                EditExerciseCommand.RaiseCanExecuteChanged();
                DeleteExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private ExerciseSearchCriteriaViewModel exerciseSearchCriteriaViewModel;
        public ExerciseSearchCriteriaViewModel ExerciseSearchCriteriaViewModel
        {
            get { return exerciseSearchCriteriaViewModel; }
            set { Set(() => ExerciseSearchCriteriaViewModel, ref exerciseSearchCriteriaViewModel, value); }
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
        public ObservableCollection<ExerciseSearchResultModel> ExerciseList { get; private set; } = new ObservableCollection<ExerciseSearchResultModel>();


        private void Find(FindExercisesMessage obj)
        {
            ExerciseList.Clear();

            var searchCriteria = Mapper.Map<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>(ExerciseSearchCriteriaViewModel);

            foreach (var exercise in exerciseService.Find(searchCriteria))
            {
                ExerciseList.Add(Mapper.Map<ExerciseSearchResultModel>(exercise));
            }
        }

        private void EditExercise()
        {
            var exercise = this.exerciseService.Get(SelectedExercise.Id);
            Messenger.Default.Send(new StartEditingExerciseMessage(exercise));
        }

        private void DeleteExercise()
        {
            exerciseService.Remove(SelectedExercise.Id);
            ExerciseList.Remove(SelectedExercise);
        }

        private void AddExercise()
        {
            Messenger.Default.Send(new StartEditingExerciseMessage(exerciseService.Create()));
        }
    }
}
