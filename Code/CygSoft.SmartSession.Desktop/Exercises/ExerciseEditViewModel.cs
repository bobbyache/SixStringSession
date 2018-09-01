using AutoMapper;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseEditViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogService dialogService;

        private ExerciseSearchResult currentExercise;
        private ExerciseSearchResult copiedExercise;

        public ExerciseSearchResult Exercise
        {
            get { return currentExercise; }
            set
            {
                copiedExercise = Mapper.Map<ExerciseSearchResult>(value);
                Set(() => Exercise, ref currentExercise, value);
            }
        }

        public ExerciseEditViewModel(IExerciseService exerciseService, IDialogService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => true);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        private void Save()
        {
            var domainExercise = Mapper.Map<Exercise>(currentExercise);
            exerciseService.Update(domainExercise);
            Messenger.Default.Send(new ExerciseEditedMessage());
        }

        private void Cancel()
        {
            Mapper.Map(copiedExercise, currentExercise);
            Messenger.Default.Send(new ExerciseEditedMessage());
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
