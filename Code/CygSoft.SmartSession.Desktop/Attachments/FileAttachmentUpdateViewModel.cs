using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentUpdateViewModel : FileAttachmentEditViewModel
    {
        public FileAttachmentUpdateViewModel(IFileAttachmentService fileAttachmentService, IDialogViewService dialogService) 
            : base(fileAttachmentService, dialogService)
        {
        }

        public override void StartEdit(int? fileAttachmentId)
        {
            if (this.FileAttachment != null) this.FileAttachment.ErrorsChanged -= FileAttachment_ErrorsChanged;

            var fileAttachmentModel = new FileAttachmentUpdateModel(this.fileAttachmentService.Get(fileAttachmentId.Value));
            fileAttachmentModel.ErrorsChanged += FileAttachment_ErrorsChanged;
            this.FileAttachment = fileAttachmentModel;
        }

        protected override void Save()
        {
            base.Save();

            if (fileAttachmentModel.Id <= 0)
            {
                throw new InvalidOperationException("An existing attachment must have an ID");
            }
            else
            {
                fileAttachmentService.Update(fileAttachmentModel.FileAttachment);
                Messenger.Default.Send(new EndEditingFileAttachmentMessage(fileAttachmentModel, false));
            }
        }
    }
}
