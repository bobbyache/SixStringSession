namespace CygSoft.SmartSession.Desktop.Attachments
{
    internal class EndEditingFileAttachmentMessage
    {
        public FileAttachmentModel FileAttachmentModel { get; }
        public bool Edited { get; }
        public bool AddingNew { get; }

        public EndEditingFileAttachmentMessage(FileAttachmentModel fileAttachmentModel, bool addingNew, bool edited = true)
        {
            FileAttachmentModel = fileAttachmentModel;
            AddingNew = addingNew;
            Edited = edited;
        }
    }
}
