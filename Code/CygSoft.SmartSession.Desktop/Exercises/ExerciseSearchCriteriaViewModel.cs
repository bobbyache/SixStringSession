using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSearchCriteriaViewModel : ViewModelBase, IExerciseSearchCriteria
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public ExerciseSearchCriteriaViewModel(IExerciseService exerciseService, IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            ResetCommand = new RelayCommand(Reset, true);
            FindCommand = new RelayCommand(Find, true);
        }

        private void Find()
        {
            Messenger.Default.Send(new FindExercisesMessage());
        }

        private void Reset()
        {
            Title = null;
            ToDateCreated = null;
            FromDateCreated = null;
            FromDateModified = null;
            ToDateModified = null;
        }

        private DateTime? dateCreatedBefore;
        public DateTime? ToDateCreated
        {
            get
            {
                return dateCreatedBefore;
            }
            set
            {
                Set(() => ToDateCreated, ref dateCreatedBefore, value);
            }
        }


        private DateTime? dateCreatedAfter;
        public DateTime? FromDateCreated
        {
            get
            {
                return dateCreatedAfter;
            }
            set
            {
                Set(() => FromDateCreated, ref dateCreatedAfter, value);
            }
        }


        private DateTime? dateModifiedAfter;
        public DateTime? FromDateModified
        {
            get
            {
                return dateModifiedAfter;
            }
            set
            {
                Set(() => FromDateModified, ref dateModifiedAfter, value);
            }
        }


        private DateTime? dateModifiedBefore;
        public DateTime? ToDateModified
        {
            get
            {
                return dateModifiedBefore;
            }
            set
            {
                Set(() => ToDateModified, ref dateModifiedBefore, value);
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
            }
        }

        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand FindCommand { get; private set; }

    }
}
