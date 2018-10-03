using AutoMapper;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchViewModel : ViewModelBase
    {
        #region Alternative Constructors
        //public FileAttachmentSearchViewModel(IFileAttachmentService fileAttachmentService, IDialogService dialogService, INavigationService navigationService)
        //{
        //    this.fileAttachmentService = fileAttachmentService;
        //    this.dialogService = dialogService;
        //    this.navigationService = navigationService;
        //}

        // for blend:
        //public FileAttachmentSearchViewModel() : this(new FileAttachmentService(), new DialogService(), new NavigationService())
        //{
        //    ...
        //}

        #endregion

        private IFileAttachmentService fileAttachmentService;
        private IDialogService dialogService;

        public FileAttachmentSearchViewModel(FileAttachmentSearchCriteriaViewModel fileAttachmentSearchCriteriaViewModel, IFileAttachmentService fileAttachmentService, IDialogService dialogService)
        {
            this.fileAttachmentSearchCriteriaViewModel = fileAttachmentSearchCriteriaViewModel ?? throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.fileAttachmentService = fileAttachmentService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddFileAttachmentCommand = new RelayCommand(AddFileAttachment, () => true);
            DeleteFileAttachmentCommand = new RelayCommand(DeleteFileAttachment, () => SelectedFileAttachment != null);
            EditFileAttachmentCommand = new RelayCommand(EditFileAttachment, () => SelectedFileAttachment != null);

            Messenger.Default.Register<FindFileAttachmentsMessage>(this, Find);
        }

        public RelayCommand AddFileAttachmentCommand { get; private set; }
        public RelayCommand DeleteFileAttachmentCommand { get; private set; }
        public RelayCommand EditFileAttachmentCommand { get; private set; }

        private FileAttachmentSearchResult selectedFileAttachment;
        public FileAttachmentSearchResult SelectedFileAttachment
        {
            get { return selectedFileAttachment; }
            set
            {
                Set(() => SelectedFileAttachment, ref selectedFileAttachment, value);
                AddFileAttachmentCommand.RaiseCanExecuteChanged();
                EditFileAttachmentCommand.RaiseCanExecuteChanged();
                DeleteFileAttachmentCommand.RaiseCanExecuteChanged();
            }
        }

        private FileAttachmentSearchCriteriaViewModel fileAttachmentSearchCriteriaViewModel;
        public FileAttachmentSearchCriteriaViewModel FileAttachmentSearchCriteriaViewModel
        {
            get { return fileAttachmentSearchCriteriaViewModel; }
            set { Set(() => FileAttachmentSearchCriteriaViewModel, ref fileAttachmentSearchCriteriaViewModel, value); }
        }

        private bool isItemsControlOpen;
        public bool IsItemsControlOpen
        {
            get
            {
                return isItemsControlOpen;
            }
            set
            {
                Set(() => IsItemsControlOpen, ref isItemsControlOpen, value);
            }
        }


        public ObservableCollection<int> DifficultyList { get; private set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        public ObservableCollection<int> PracticalityList { get; private set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        public ObservableCollection<FileAttachmentSearchResult> FileAttachmentList { get; private set; } = new ObservableCollection<FileAttachmentSearchResult>();


        private void Find(FindFileAttachmentsMessage obj)
        {
            FileAttachmentList.Clear();

            var searchCriteria = Mapper.Map<FileAttachmentSearchCriteriaViewModel, FileAttachmentSearchCriteria>(FileAttachmentSearchCriteriaViewModel);

            foreach (var fileAttachment in fileAttachmentService.Find(searchCriteria))
            {
                FileAttachmentList.Add(Mapper.Map<FileAttachmentSearchResult>(fileAttachment));
            }
        }

        private void EditFileAttachment()
        {
            Messenger.Default.Send(new StartEditingFileAttachmentMessage(SelectedFileAttachment));
            //dialogService.ShowMessage($"Edited - {DateTime.Now}. This is an extra little note.", "Edit");
        }

        private void DeleteFileAttachment()
        {
            fileAttachmentService.Remove(SelectedFileAttachment.Id);
            FileAttachmentList.Remove(SelectedFileAttachment);
        }

        private void AddFileAttachment()
        {
            var fileAttachment = new FileAttachmentSearchResult
            {
                FileTitle = $"New FileAttachment Item - {DateTime.Now}",
    
                Notes = null
            };

            var domainFileAttachment = Mapper.Map<FileAttachment>(fileAttachment);
            fileAttachmentService.Add(domainFileAttachment);
            Mapper.Map(domainFileAttachment, fileAttachment);

            FileAttachmentList.Add(fileAttachment);
            SelectedFileAttachment = fileAttachment;
        }
    }
}
