using AutoMapper;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSearchViewModel : ViewModelBase
    {

        #region Alternative Constructors
        //public ExerciseSearchViewModel(IExerciseService exerciseService, IDialogService dialogService, INavigationService navigationService)
        //{
        //    this.exerciseService = exerciseService;
        //    this.dialogService = dialogService;
        //    this.navigationService = navigationService;
        //}

        // for blend:
        //public ExerciseSearchViewModel() : this(new ExerciseService(), new DialogService(), new NavigationService())
        //{
        //    ...
        //}

        #endregion

        private IExerciseService exerciseService;
        private IDialogService dialogService;

        public ExerciseSearchViewModel(ExerciseSearchCriteriaViewModel exerciseSearchCriteriaViewModel, IExerciseService exerciseService, IDialogService dialogService)
        {
            this.exerciseSearchCriteriaViewModel = exerciseSearchCriteriaViewModel ?? throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddExerciseCommand = new RelayCommand(AddExercise, () => true);
            DeleteExerciseCommand = new RelayCommand(DeleteExercise, () => SelectedExercise != null);
            EditExerciseCommand = new RelayCommand(EditExercise, () => SelectedExercise != null);

            Messenger.Default.Register<FindExercisesMessage>(this, Find);
        }

        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteExerciseCommand { get; private set; }
        public RelayCommand EditExerciseCommand { get; private set; }

        private ExerciseSearchResult selectedExercise;
        public ExerciseSearchResult SelectedExercise
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
        public ObservableCollection<ExerciseSearchResult> ExerciseList { get; private set; } = new ObservableCollection<ExerciseSearchResult>();


        private void Find(FindExercisesMessage obj)
        {
            ExerciseList.Clear();

            var searchCriteria = Mapper.Map<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>(ExerciseSearchCriteriaViewModel);

            foreach (var exercise in exerciseService.Find(searchCriteria))
            {
                ExerciseList.Add(Mapper.Map<ExerciseSearchResult>(exercise));
            }
        }

        private void EditExercise()
        {
            Messenger.Default.Send(new EditExerciseMessage(SelectedExercise));
            //dialogService.ShowMessage($"Edited - {DateTime.Now}. This is an extra little note.", "Edit");
        }

        private void DeleteExercise()
        {
            exerciseService.Remove(SelectedExercise.Id);
            ExerciseList.Remove(SelectedExercise);
        }

        private void AddExercise()
        {
            var exercise = new ExerciseSearchResult
            {
                Title = $"New Exercise Item - {DateTime.Now}",
                DifficultyRating = 0,
                PracticalityRating = 0,
                Scribed = false,
                OptimalDuration = 300, //(5 x 60 secs),
                Notes = null
            };

            var domainExercise = Mapper.Map<Exercise>(exercise);
            exerciseService.Add(domainExercise);
            Mapper.Map(domainExercise, exercise);

            ExerciseList.Add(exercise);
            SelectedExercise = exercise;
        }
    }
}
