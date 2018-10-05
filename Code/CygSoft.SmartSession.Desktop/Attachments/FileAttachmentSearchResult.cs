using GalaSoft.MvvmLight;


namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchResult : ObservableObject
    {
        public int Id { get; set; }

        public string FileName { get { return FileTitle + Extension; } }

        private string fileTitle;
        public string FileTitle
        {
            get { return fileTitle; }
            set
            {
                Set(() => FileTitle, ref fileTitle, value);
            }
        }

        private string extension;
        public string Extension
        {
            get { return extension; }
            set
            {
                Set(() => Extension, ref extension, value);
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                Set(() => Notes, ref notes, value);
            }
        }
    }
}
