using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseEditViewModel : ValidatableViewModel
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        private ExerciseSearchResultModel exerciseSearchResult;

        public Exercise Exercise { get; private set; }

        public int Id { get; set; }

        private string title;
        [Required]
        public string Title
        {
            get { return title; }
            set
            {
                Set(() => Title, ref title, value, true, true);
            }
        }

        private PercentCompleteCalculationStrategy percentageCompleteCalculationType;
        public PercentCompleteCalculationStrategy PercentageCompleteCalculationType
        {
            get { return percentageCompleteCalculationType; }
            set
            {
                Set(() => PercentageCompleteCalculationType, ref percentageCompleteCalculationType, value, true, true);
            }
        }

        private int? initialMetronomeSpeed;
        public int? InitialMetronomeSpeed
        {
            get
            {
                return initialMetronomeSpeed;
            }
            set
            {
                Set(() => InitialMetronomeSpeed, ref initialMetronomeSpeed, value);
            }
        }

        private int targetMetronomeSpeed;
        [Range(0, 250, ErrorMessage = "Value must be between 0 and 250.")]
        public int TargetMetronomeSpeed
        {
            get { return targetMetronomeSpeed; }
            set
            {
                Set(() => TargetMetronomeSpeed, ref targetMetronomeSpeed, value, true, true);
            }
        }

        private int targetPracticeTime;
        [Range(0, 10000, ErrorMessage = "Value must be between 0 and 10,000.")]
        public int TargetPracticeTime
        {
            get { return targetPracticeTime; }
            set
            {
                Set(() => TargetPracticeTime, ref targetPracticeTime, value, true, true);
            }
        }

        private int difficultyRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int DifficultyRating
        {
            get { return difficultyRating; }
            set
            {
                Set(() => DifficultyRating, ref difficultyRating, value, true, true);
            }
        }

        private int practicalityRating;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5.")]
        public int PracticalityRating
        {
            get { return practicalityRating; }
            set
            {
                Set(() => PracticalityRating, ref practicalityRating, value, true, true);
            }
        }

        public override void Commit()
        {
            Mapper.Map(this, Exercise);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(Exercise, this);
            base.Revert();
        }

        public ExerciseEditViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !this.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
            DeleteFilesCommand = new RelayCommand(() => DeleteFiles(), () => true);
            OpenFileCommand = new RelayCommand(() => OpenFile(), () => true);
        }

        private void OpenFile()
        {
            exerciseService.OpenFile(Exercise.Id, null);
        }

        public void DeleteFiles()
        {
            exerciseService.DeleteFiles(Exercise.Id);
        }


        public void AddFiles(string[] files)
        {
            exerciseService.AddFiles(Exercise.Id, files);
        }

        public void StartEdit(ExerciseSearchResultModel exerciseSearchResult)
        {
            this.exerciseSearchResult = exerciseSearchResult;

            if (this.Exercise != null) this.ErrorsChanged -= Exercise_ErrorsChanged;
            this.Exercise = this.exerciseService.Get(exerciseSearchResult.Id);

            this.Revert();

            TrackChanges = true;

            this.ErrorsChanged += Exercise_ErrorsChanged;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            this.Commit();
            exerciseService.Update(Exercise);

            if (exerciseSearchResult != null)
                Mapper.Map(this.Exercise, exerciseSearchResult);

            Messenger.Default.Send(new EndEditingExerciseMessage(Exercise.Id));
        }

        private void Cancel()
        {
            this.Revert();

            if (exerciseSearchResult != null)
                Mapper.Map(this.Exercise, exerciseSearchResult);

            Messenger.Default.Send(new EndEditingExerciseMessage(Exercise.Id));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand DeleteFilesCommand { get; private set; }

        public RelayCommand OpenFileCommand { get; private set; }
    }
}
