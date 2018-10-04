using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseEditViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        private ExerciseModel exerciseModel;
        private ExerciseSearchResult exerciseSearchResult;

        public ExerciseModel Exercise
        {
            get { return exerciseModel; }
            private set
            {
                Set(() => Exercise, ref exerciseModel, value);
            }
        }

        public ExerciseEditViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !exerciseModel.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public void StartEdit(ExerciseSearchResult exerciseSearchResult)
        {
            this.exerciseSearchResult = exerciseSearchResult;

            if (this.Exercise != null) this.Exercise.ErrorsChanged -= Exercise_ErrorsChanged;
            this.Exercise = new ExerciseModel(this.exerciseService.Get(exerciseSearchResult.Id));
            this.Exercise.ErrorsChanged += Exercise_ErrorsChanged;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            exerciseModel.Commit();
            exerciseService.Update(exerciseModel.Exercise);

            if (exerciseSearchResult != null)
                Mapper.Map(exerciseModel, exerciseSearchResult);

            Messenger.Default.Send(new EndEditingExerciseMessage(exerciseModel));
        }

        private void Cancel()
        {
            exerciseModel.Revert();

            if (exerciseSearchResult != null)
                Mapper.Map(exerciseModel, exerciseSearchResult);

            Messenger.Default.Send(new EndEditingExerciseMessage(exerciseModel));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
