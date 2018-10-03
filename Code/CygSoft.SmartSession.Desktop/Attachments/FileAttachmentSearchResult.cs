using GalaSoft.MvvmLight;


namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchResult : ObservableObject
    {
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { Set(() => IsDirty, ref isDirty, value); }
        }

        public int Id { get; set; }

        private string fileTitle;
        public string FileTitle
        {
            get { return fileTitle; }
            set
            {
                if (Set(() => FileTitle, ref fileTitle, value))
                    isDirty = true;
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                if (Set(() => Notes, ref notes, value))
                    isDirty = true;
            }
        }
    }
}
