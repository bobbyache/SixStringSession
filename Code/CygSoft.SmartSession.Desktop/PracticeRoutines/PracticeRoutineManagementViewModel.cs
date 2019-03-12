using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineManagementViewModel : ViewModelBase
    {
        private IPracticeRoutineService practiceRoutineService;
        private IDialogViewService dialogService;

        public PracticeRoutineManagementViewModel(IPracticeRoutineService practiceRoutineService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            PracticeCommand = new RelayCommand(Practice, () => SelectedPracticeRoutine != null);
            AddPracticeRoutineCommand = new RelayCommand(AddPracticeRoutine, () => true);
            DeletePracticeRoutineCommand = new RelayCommand(DeletePracticeRoutine, () => SelectedPracticeRoutine != null);
            EditPracticeRoutineCommand = new RelayCommand(EditPracticeRoutine, () => SelectedPracticeRoutine != null);
        }

        public RelayCommand AddPracticeRoutineCommand { get; private set; }
        public RelayCommand DeletePracticeRoutineCommand { get; private set; }
        public RelayCommand EditPracticeRoutineCommand { get; private set; }
        public RelayCommand PracticeCommand { get; private set; }

        private PracticeRoutineSearchResultModel selectedPracticeRoutine;
        public PracticeRoutineSearchResultModel SelectedPracticeRoutine
        {
            get { return selectedPracticeRoutine; }
            set
            {
                Set(() => SelectedPracticeRoutine, ref selectedPracticeRoutine, value);
                PracticeCommand.RaiseCanExecuteChanged();
                AddPracticeRoutineCommand.RaiseCanExecuteChanged();
                EditPracticeRoutineCommand.RaiseCanExecuteChanged();
                DeletePracticeRoutineCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PracticeRoutineSearchResultModel> PracticeRoutineList { get; private set; } = new ObservableCollection<PracticeRoutineSearchResultModel>();

        private void Practice()
        {
            if (SelectedPracticeRoutine != null)
                Messenger.Default.Send(new ViewPracticeListMessage(SelectedPracticeRoutine.Id));
        }

        public void RefreshRoutines()
        {
            PracticeRoutineList.Clear();

            foreach (var practiceRoutine in practiceRoutineService.Find(new PracticeRoutineSearchCriteria()))
            {
                PracticeRoutineList.Add(Mapper.Map<PracticeRoutineSearchResultModel>(practiceRoutine));
            }
        }

        private void EditPracticeRoutine()
        {
            var practiceRoutine = this.practiceRoutineService.Get(SelectedPracticeRoutine.Id);
            Messenger.Default.Send(new StartEditingPracticeRoutineMessage(practiceRoutine));
        }

        private void DeletePracticeRoutine()
        {
            practiceRoutineService.Remove(SelectedPracticeRoutine.Id);
            PracticeRoutineList.Remove(SelectedPracticeRoutine);
        }

        private void AddPracticeRoutine()
        {
            Messenger.Default.Send(new StartEditingPracticeRoutineMessage(practiceRoutineService.Create()));
        }
    }
}
