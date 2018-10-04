using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.GoalTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    public class GoalTaskSearchCriteriaViewModel : ViewModelBase, IGoalTaskSearchCriteria
    {
        private IGoalTaskService goalTaskService;
        private IDialogViewService dialogService;

        public GoalTaskSearchCriteriaViewModel(IGoalTaskService goalTaskService, IDialogViewService dialogService)
        {
            this.goalTaskService = goalTaskService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            ResetCommand = new RelayCommand(Reset, true);
            FindCommand = new RelayCommand(Find, true);
        }

        private void Find()
        {
            Messenger.Default.Send(new FindGoalTasksMessage());
        }

        private void Reset()
        {
            Title = null;
            HasNotes = null;
            DateCreatedBefore = null;
            DateCreatedAfter = null;
            DateModifiedAfter = null;
            DateModifiedBefore = null;
        }

        private DateTime? dateCreatedBefore;
        public DateTime? DateCreatedBefore
        {
            get
            {
                return dateCreatedBefore;
            }
            set
            {
                Set(() => DateCreatedBefore, ref dateCreatedBefore, value);
            }
        }

        private DateTime? dateCreatedAfter;
        public DateTime? DateCreatedAfter
        {
            get
            {
                return dateCreatedAfter;
            }
            set
            {
                Set(() => DateCreatedAfter, ref dateCreatedAfter, value);
            }
        }

        private DateTime? dateModifiedAfter;
        public DateTime? DateModifiedAfter
        {
            get
            {
                return dateModifiedAfter;
            }
            set
            {
                Set(() => DateModifiedAfter, ref dateModifiedAfter, value);
            }
        }

        private DateTime? dateModifiedBefore;
        public DateTime? DateModifiedBefore
        {
            get
            {
                return dateModifiedBefore;
            }
            set
            {
                Set(() => DateModifiedBefore, ref dateModifiedBefore, value);
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

        private bool? hasNotes;
        public bool? HasNotes
        {
            get
            {
                return hasNotes;
            }
            set
            {
                Set(() => HasNotes, ref hasNotes, value);
            }
        }

        private string keywords;
        public string Keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                Set(() => Keywords, ref keywords, value);
            }
        }

        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand FindCommand { get; private set; }

    }
}
