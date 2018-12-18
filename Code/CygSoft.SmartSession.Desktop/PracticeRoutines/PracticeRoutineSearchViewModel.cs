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
    public class PracticeRoutineSearchViewModel : ViewModelBase
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

        public PracticeRoutineSearchViewModel(IPracticeRoutineService practiceRoutineService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddPracticeRoutineCommand = new RelayCommand(AddPracticeRoutine, () => true);
            DeletePracticeRoutineCommand = new RelayCommand(DeletePracticeRoutine, () => SelectedPracticeRoutine != null);
            EditPracticeRoutineCommand = new RelayCommand(EditPracticeRoutine, () => SelectedPracticeRoutine != null);
        }

        public RelayCommand AddPracticeRoutineCommand { get; private set; }
        public RelayCommand DeletePracticeRoutineCommand { get; private set; }
        public RelayCommand EditPracticeRoutineCommand { get; private set; }

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
        public ObservableCollection<PracticeRoutineSearchResultModel> PracticeRoutineList { get; private set; } = new ObservableCollection<PracticeRoutineSearchResultModel>();


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

            //var domainPracticeRoutine = Mapper.Map<PracticeRoutine>(practiceRoutine);
            //practiceRoutineService.Add(domainPracticeRoutine);
            //Mapper.Map(domainPracticeRoutine, practiceRoutine);

            //PracticeRoutineList.Add(practiceRoutine);
            //SelectedPracticeRoutine = practiceRoutine;
        }

    }
}
