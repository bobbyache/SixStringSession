using GalaSoft.MvvmLight;


namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchResultModel : ObservableObject
    {
        public int Id { get; set; }

        public string FileName
        {
            get
            {
                if (FileId == null || Extension == null)
                    return null;
                return FileId + Extension;
            }
        }

        private string fileId;
        public string FileId
        {
            get { return fileId; }
            set
            {
                Set(() => FileId, ref fileId, value);
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                Set(() => Title, ref title, value);
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
