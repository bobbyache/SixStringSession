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

        public ExerciseSearchViewModel(IExerciseService exerciseService, IDialogService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddExerciseCommand = new RelayCommand(AddExercise, () => true);
            DeleteExerciseCommand = new RelayCommand(DeleteExercise, () => SelectedExercise != null);
            EditExerciseCommand = new RelayCommand(EditExercise, () => SelectedExercise != null);
            SearchCommand = new RelayCommand<string>(Search, true);

            Search(null);
        }

        public RelayCommand<string> SearchCommand { get; private set; }
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

        public ObservableCollection<ExerciseSearchResult> ExerciseList { get; private set; } = new ObservableCollection<ExerciseSearchResult>();

        private void Search(string titleFragment)
        {
            ExerciseList.Clear();

            foreach (var exercise in exerciseService.Find(titleFragment))
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
            exerciseService.Delete(SelectedExercise.Id);
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
