using AutoMapper;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentEditViewModel : ViewModelBase
    {
        private IFileAttachmentService fileAttachmentService;
        private IDialogService dialogService;

        private FileAttachmentModel fileAttachmentModel;
        private FileAttachmentSearchResult fileAttachmentSearchResult;

        public FileAttachmentModel FileAttachment
        {
            get { return fileAttachmentModel; }
            private set
            {
                Set(() => FileAttachment, ref fileAttachmentModel, value);
            }
        }

        public FileAttachmentEditViewModel(IFileAttachmentService fileAttachmentService, IDialogService dialogService)
        {
            this.fileAttachmentService = fileAttachmentService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !fileAttachmentModel.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public void StartEdit(FileAttachmentSearchResult fileAttachmentSearchResult)
        {
            this.fileAttachmentSearchResult = fileAttachmentSearchResult;

            if (this.FileAttachment != null) this.FileAttachment.ErrorsChanged -= FileAttachment_ErrorsChanged;
            this.FileAttachment = new FileAttachmentModel(this.fileAttachmentService.Get(fileAttachmentSearchResult.Id));
            this.FileAttachment.ErrorsChanged += FileAttachment_ErrorsChanged;
        }

        private void FileAttachment_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            fileAttachmentModel.Commit();
            fileAttachmentService.Update(fileAttachmentModel.FileAttachment);

            if (fileAttachmentSearchResult != null)
                Mapper.Map(fileAttachmentModel, fileAttachmentSearchResult);

            Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel));
        }

        private void Cancel()
        {
            fileAttachmentModel.Revert();

            if (fileAttachmentSearchResult != null)
                Mapper.Map(fileAttachmentModel, fileAttachmentSearchResult);

            Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
