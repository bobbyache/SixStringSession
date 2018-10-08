using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentCompositeViewModel : ViewModelBase
    {
        private FileAttachmentSearchViewModel fileAttachmentSearchViewModel;
        private FileAttachmentEditViewModel fileAttachmentEditViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public FileAttachmentCompositeViewModel(FileAttachmentSearchViewModel fileAttachmentSearchViewModel, FileAttachmentEditViewModel fileAttachmentEditViewModel)
        {
            this.fileAttachmentSearchViewModel = fileAttachmentSearchViewModel;
            this.fileAttachmentEditViewModel = fileAttachmentEditViewModel;

            Messenger.Default.Register<StartEditingFileAttachmentMessage>(this, (m) => StartEditingFileAttachment(m.FileAttachmentSearchResult));
            Messenger.Default.Register<EndEditingFileAttachmentMessage>(this, (m) => EndEditingFileAttachment(m.FileAttachmentModel));

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("Search");
        }

        private void EndEditingFileAttachment(FileAttachmentModel fileAttachmentModel)
        {
            OnNavigation("Search");
        }


        private void StartEditingFileAttachment(FileAttachmentSearchResultModel fileAttachmentSearchResult)
        {
            fileAttachmentEditViewModel.StartEdit(fileAttachmentSearchResult?.Id);
            OnNavigation("Edit");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = fileAttachmentSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = fileAttachmentEditViewModel;
                    break;
                default:
                    CurrentViewModel = fileAttachmentSearchViewModel;
                    break;
            }
        }
    }
}
