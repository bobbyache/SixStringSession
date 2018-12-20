using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineRecorderViewModel : ViewModelBase
    {
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public PracticeRoutineRecorderViewModel()
        {
            SaveCommand = new RelayCommand(() => Save(), () => true);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        private void Cancel()
        {
            Messenger.Default.Send(new CancelRecordingPracticeRoutineMessage());
        }

        private void Save()
        {
            Messenger.Default.Send(new SavePracticeRoutineRecordingMessage());
        }
    }
}
