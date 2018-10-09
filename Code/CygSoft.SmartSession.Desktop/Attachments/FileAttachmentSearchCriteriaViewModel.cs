using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchCriteriaViewModel : ViewModelBase, IFileAttachmentSearchCriteria
    {
        private IFileAttachmentService fileAttachmentService;
        private IDialogViewService dialogService;

        public FileAttachmentSearchCriteriaViewModel(IFileAttachmentService fileAttachmentService, IDialogViewService dialogService)
        {
            this.fileAttachmentService = fileAttachmentService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            ResetCommand = new RelayCommand(Reset, true);
            FindCommand = new RelayCommand(Find, true);
        }

        private void Find()
        {
            Messenger.Default.Send(new FindFileAttachmentsMessage());
        }

        private void Reset()
        {
            FileTitle = null;
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

        private string fileTitle;
        public string FileTitle
        {
            get
            {
                return fileTitle;
            }
            set
            {
                Set(() => FileTitle, ref fileTitle, value);
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

        private string extension;
        public string Extension
        {
            get
            {
                return extension;
            }
            set
            {
                Set(() => Extension, ref extension, value);
            }
        }


        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand FindCommand { get; private set; }
        
    }
}
