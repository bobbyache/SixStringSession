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
    public class PracticeRoutineRecordingListViewModel : ViewModelBase
    {
        private IPracticeRoutineService practiceRoutineService;
        private IDialogViewService dialogService;

        public RelayCommand PracticeCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

        public PracticeRoutineRecordingListViewModel(IPracticeRoutineService practiceRoutineService, IDialogViewService dialogService)
        {
            this.practiceRoutineService = practiceRoutineService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            PracticeCommand = new RelayCommand(Practice, () => true);
            ExitCommand = new RelayCommand(Exit, () => true);
        }

        private void Exit()
        {
            Messenger.Default.Send(new ExitPracticeListMessage());
        }

        private void Practice()
        {
            Messenger.Default.Send(new ExitPracticeListMessage());
        }
    }
}
