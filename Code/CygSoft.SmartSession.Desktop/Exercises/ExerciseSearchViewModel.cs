using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
            SearchCommand = new RelayCommand(Search, true);

            Search();
        }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteExerciseCommand { get; private set; }
        public RelayCommand EditExerciseCommand { get; private set; }

        private ExerciseListItem selectedExercise;
        public ExerciseListItem SelectedExercise
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

        public ObservableCollection<ExerciseListItem> ExerciseList { get; private set; } = new ObservableCollection<ExerciseListItem>();

        private void Search()
        {
            ExerciseList.Clear();

            foreach (var exercise in exerciseService.Find(""))
            {
                var listItem = new ExerciseListItem()
                {
                    Id = exercise.Id,
                    Title = exercise.Title,
                    DifficultyRating = exercise.DifficultyRating,
                    OptimalDuration = exercise.OptimalDuration,
                    PracticalityRating = exercise.PracticalityRating,
                    Scribed = exercise.Scribed,
                    Notes = exercise.Notes
                };
                ExerciseList.Add(listItem);
            }
        }

        private void EditExercise()
        {
            dialogService.ShowMessage("Editing...", "Edit");

            SelectedExercise.Title = $"Edited - {DateTime.Now}";
            SelectedExercise.Notes = $"Edited - {DateTime.Now}. This is an extra little note.";

            exerciseService.Update(new Exercise
            {
                Id = SelectedExercise.Id,
                Title = SelectedExercise.Title,
                DifficultyRating = SelectedExercise.DifficultyRating,
                PracticalityRating = SelectedExercise.PracticalityRating,
                Scribed = SelectedExercise.Scribed,
                OptimalDuration = SelectedExercise.OptimalDuration,
                Notes = SelectedExercise.Notes
            });
        }

        private void DeleteExercise()
        {
            exerciseService.Delete(SelectedExercise.Id);
            ExerciseList.Remove(SelectedExercise);
        }

        private void AddExercise()
        {
            var exercise = new ExerciseListItem
            {
                Title = $"Added Exercise Item - {DateTime.Now}",
                DifficultyRating = 3,
                PracticalityRating = 4,
                Scribed = false,
                OptimalDuration = 120,
                Notes = $"Here is a sample note - {DateTime.Now}"
            };

            exerciseService.Add(new Exercise()
            {
                Title = exercise.Title,
                OptimalDuration = exercise.OptimalDuration,
                DifficultyRating = exercise.DifficultyRating,
                PracticalityRating = exercise.PracticalityRating,
                Scribed = exercise.Scribed,
                Notes = exercise.Notes
            });
            ExerciseList.Add(exercise);
            SelectedExercise = exercise;
        }
    }
}
