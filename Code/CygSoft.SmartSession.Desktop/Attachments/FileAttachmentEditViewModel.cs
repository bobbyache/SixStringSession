using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public abstract class FileAttachmentEditViewModel : ViewModelBase
    {
        protected IFileAttachmentService fileAttachmentService;
        protected IDialogViewService dialogService;

        protected FileAttachmentModel fileAttachmentModel;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        

        public FileAttachmentModel FileAttachment
        {
            get { return fileAttachmentModel; }
            protected set
            {
                Set(() => FileAttachment, ref fileAttachmentModel, value);
            }
        }

        public FileAttachmentEditViewModel(IFileAttachmentService fileAttachmentService, IDialogViewService dialogService)
        {
            this.fileAttachmentService = fileAttachmentService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !fileAttachmentModel.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public virtual void StartEdit(int? fileAttachmentId)
        {            
        }

        protected void FileAttachment_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        protected virtual void Save()
        {
            fileAttachmentModel.Commit();
        }

        private void Cancel()
        {
            fileAttachmentModel.Revert();
            Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, false, false));
        }
    }
}
