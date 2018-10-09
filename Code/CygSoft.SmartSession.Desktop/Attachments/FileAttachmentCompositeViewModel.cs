using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentCompositeViewModel : ViewModelBase
    {
        private FileAttachmentSearchViewModel fileAttachmentSearchViewModel;
        private FileAttachmentCreateViewModel fileAttachmentCreateViewModel;
        private FileAttachmentUpdateViewModel fileAttachmentUpdateViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public FileAttachmentCompositeViewModel(FileAttachmentSearchViewModel fileAttachmentSearchViewModel, 
            FileAttachmentCreateViewModel fileAttachmentCreateViewModel, FileAttachmentUpdateViewModel fileAttachmentUpdateViewModel)
        {
            this.fileAttachmentSearchViewModel = fileAttachmentSearchViewModel;
            this.fileAttachmentCreateViewModel = fileAttachmentCreateViewModel;
            this.fileAttachmentUpdateViewModel = fileAttachmentUpdateViewModel;

            Messenger.Default.Register<StartEditingFileAttachmentMessage>(this, (m) => StartEditingFileAttachment(m.FileAttachmentSearchResult, m.Mode));
            Messenger.Default.Register<EndEditingFileAttachmentMessage>(this, (m) => EndEditingFileAttachment(m.FileAttachmentModel));

            NavigateToSearchView();
        }

        private void EndEditingFileAttachment(FileAttachmentModel fileAttachmentModel)
        {
            NavigateToSearchView();
        }

        private void NavigateToSearchView()
        {
            CurrentViewModel = fileAttachmentSearchViewModel;
        }

        private void StartEditingFileAttachment(FileAttachmentSearchResultModel fileAttachmentSearchResult, StartEditingEntityMode mode)
        {
            if (mode == StartEditingEntityMode.Create)
            {
                fileAttachmentCreateViewModel.StartEdit(null);
                CurrentViewModel = fileAttachmentCreateViewModel;
            }

            else
            {
                fileAttachmentUpdateViewModel.StartEdit(fileAttachmentSearchResult?.Id);
                CurrentViewModel = fileAttachmentUpdateViewModel;
            }
        }
    }
}
