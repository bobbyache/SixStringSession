using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseEditViewModel : ViewModelBase
    {
        public ExerciseEditViewModel()
        {
            SaveCommand = new RelayCommand(() => Messenger.Default.Send(new ExerciseEditedMessage()), () => true);
            CancelCommand = new RelayCommand(() => Messenger.Default.Send(new ExerciseEditedMessage()), () => true);
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
