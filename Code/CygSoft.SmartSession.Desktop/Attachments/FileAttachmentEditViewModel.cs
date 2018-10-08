using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentEditViewModel : ViewModelBase
    {
        private IFileAttachmentService fileAttachmentService;
        private IDialogViewService dialogService;

        private FileAttachmentModel fileAttachmentModel;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand OpenFileCommand { get; private set; }

        public FileAttachmentModel FileAttachment
        {
            get { return fileAttachmentModel; }
            private set
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
            OpenFileCommand = new RelayCommand(() => OpenFile());
        }

        private void OpenFile()
        {
            string filePath;
            if (dialogService.SelectFile(null, out filePath))
                fileAttachmentModel.FilePath = filePath;
        }

        public void StartEdit(int? fileAttachmentId)
        {
            if (this.FileAttachment != null) this.FileAttachment.ErrorsChanged -= FileAttachment_ErrorsChanged;

            if (fileAttachmentId.HasValue)
            {
                var fileAttachmentModel = new FileAttachmentModel(this.fileAttachmentService.Get(fileAttachmentId.Value));
                fileAttachmentModel.ErrorsChanged += FileAttachment_ErrorsChanged;
                this.FileAttachment = fileAttachmentModel;
            }
            else
            {
                var fileAttachmentModel = new FileAttachmentModel(new FileAttachment());
                fileAttachmentModel.ErrorsChanged += FileAttachment_ErrorsChanged;
                this.FileAttachment = fileAttachmentModel;
            }
            
        }

        private void FileAttachment_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            fileAttachmentModel.Commit();
            if (fileAttachmentModel.Id <= 0)
            {
                fileAttachmentService.Add(fileAttachmentModel.FileAttachment);
                fileAttachmentModel.Id = fileAttachmentModel.FileAttachment.Id;
                Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, true));
            }
            else
            {
                fileAttachmentService.Update(fileAttachmentModel.FileAttachment);
                Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, false));
            }
        }

        private void Cancel()
        {
            fileAttachmentModel.Revert();
            Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, false, false));
        }
    }
}
