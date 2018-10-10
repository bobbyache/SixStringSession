using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentCreateViewModel : FileAttachmentEditViewModel
    {
        public RelayCommand OpenFileCommand { get; private set; }

        public FileAttachmentCreateViewModel(IFileAttachmentService fileAttachmentService, IDialogViewService dialogService) 
            : base(fileAttachmentService, dialogService)
        {
            OpenFileCommand = new RelayCommand(() => OpenFile());
        }

        private void OpenFile()
        {
            string filePath;
            if (dialogService.SelectFile(null, out filePath))
                ((FileAttachmentCreateModel)fileAttachmentModel).SourceFilePath = filePath;
        }

        public override void StartEdit(int? fileAttachmentId)
        {
            if (this.FileAttachment != null) this.FileAttachment.ErrorsChanged -= FileAttachment_ErrorsChanged;

            var fileAttachmentModel = new FileAttachmentCreateModel(new FileAttachment());
            fileAttachmentModel.ErrorsChanged += FileAttachment_ErrorsChanged;
            this.FileAttachment = fileAttachmentModel;
        }

        protected override void Save()
        {
            base.Save();

            if (fileAttachmentModel.Id <= 0)
            {
                fileAttachmentService.Add(fileAttachmentModel.FileAttachment);
                fileAttachmentModel.Id = fileAttachmentModel.FileAttachment.Id;
                ((FileAttachmentCreateModel)fileAttachmentModel).SourceFilePath = null;

                Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, true));
            }
            else
            {
                throw new InvalidOperationException("Cannot create an attachment with an existing ID.");
            }
        }
    }
}
