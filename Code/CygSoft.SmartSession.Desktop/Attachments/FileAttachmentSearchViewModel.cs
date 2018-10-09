using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Attachments
{
    public class FileAttachmentSearchViewModel : ViewModelBase
    {
        private IFileAttachmentService fileAttachmentService;
        private IDialogViewService dialogService;

        public FileAttachmentSearchViewModel(FileAttachmentSearchCriteriaViewModel fileAttachmentSearchCriteriaViewModel, IFileAttachmentService fileAttachmentService, IDialogViewService dialogService)
        {
            this.fileAttachmentSearchCriteriaViewModel = fileAttachmentSearchCriteriaViewModel ?? throw new ArgumentNullException("Search Criteria Model must be provided.");
            this.fileAttachmentService = fileAttachmentService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddFileAttachmentCommand = new RelayCommand(AddFileAttachment, () => true);
            DeleteFileAttachmentCommand = new RelayCommand(DeleteFileAttachment, () => SelectedFileAttachment != null);
            EditFileAttachmentCommand = new RelayCommand(UpdateFileAttachment, () => SelectedFileAttachment != null);

            Messenger.Default.Register<FindFileAttachmentsMessage>(this, Find);
            Messenger.Default.Register<EndEditingFileAttachmentMessage>(this, UpdateEditedAttachment);
        }

        private void UpdateEditedAttachment(EndEditingFileAttachmentMessage obj)
        {
            if (!obj.Edited)
                return;

            if (obj.AddingNew)
            {
                var result = Mapper.Map<FileAttachmentSearchResultModel>(obj.FileAttachmentModel);
                FileAttachmentList.Add(result);
                SelectedFileAttachment = result;
            }
            else
            {
                if (obj.FileAttachmentModel.Id != SelectedFileAttachment.Id)
                    throw new InvalidOperationException();

                Mapper.Map(obj.FileAttachmentModel, SelectedFileAttachment);
            }
        }

        public RelayCommand AddFileAttachmentCommand { get; private set; }
        public RelayCommand DeleteFileAttachmentCommand { get; private set; }
        public RelayCommand EditFileAttachmentCommand { get; private set; }

        private FileAttachmentSearchResultModel selectedFileAttachment;
        public FileAttachmentSearchResultModel SelectedFileAttachment
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
        public ObservableCollection<FileAttachmentSearchResultModel> FileAttachmentList { get; private set; } = new ObservableCollection<FileAttachmentSearchResultModel>();


        private void Find(FindFileAttachmentsMessage obj)
        {
            FileAttachmentList.Clear();

            var searchCriteria = Mapper.Map<FileAttachmentSearchCriteriaViewModel, FileAttachmentSearchCriteria>(FileAttachmentSearchCriteriaViewModel);

            foreach (var fileAttachment in fileAttachmentService.Find(searchCriteria))
            {
                FileAttachmentList.Add(Mapper.Map<FileAttachmentSearchResultModel>(fileAttachment));
            }
        }

        private void UpdateFileAttachment()
        {
            Messenger.Default.Send(new StartEditingFileAttachmentMessage(SelectedFileAttachment, StartEditingEntityMode.Update));
        }

        private void DeleteFileAttachment()
        {
            fileAttachmentService.Remove(SelectedFileAttachment.Id);
            FileAttachmentList.Remove(SelectedFileAttachment);
        }

        private void AddFileAttachment()
        {
            Messenger.Default.Send(new StartEditingFileAttachmentMessage(null, StartEditingEntityMode.Create));
        }
    }
}
