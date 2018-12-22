using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineManagementViewModel : ViewModelBase
    {

        #region Alternative Constructors
        //public PracticeRoutineSearchViewModel(IPracticeRoutineService practiceRoutineService, IDialogViewService dialogService, INavigationService navigationService)
        //{
        //    this.practiceRoutineService = practiceRoutineService;
        //    this.dialogService = dialogService;
        //    this.navigationService = navigationService;
        //}

        // for blend:
        //public PracticeRoutineSearchViewModel() : this(new PracticeRoutineService(), new DialogService(), new NavigationService())
        //{
        //    ...
        //}

        #endregion

        private IPracticeRoutineService practiceRoutineService;
        private IDialogViewService dialogService;

        public PracticeRoutineManagementViewModel(IPracticeRoutineService practiceRoutineService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            PracticeCommand = new RelayCommand(Practice, () => true);
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
                AddPracticeRoutineCommand.RaiseCanExecuteChanged();
                EditPracticeRoutineCommand.RaiseCanExecuteChanged();
                DeletePracticeRoutineCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PracticeRoutineSearchResultModel> PracticeRoutineList { get; private set; } = new ObservableCollection<PracticeRoutineSearchResultModel>();

        private void Practice()
        {
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
