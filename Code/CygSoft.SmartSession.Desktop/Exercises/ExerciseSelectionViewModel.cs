using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseSelectionViewModel : ExerciseSearchViewModel
    {
        public RelayCommand OkCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public ExerciseSelectionViewModel(IExerciseService exerciseService,
            IDialogViewService dialogService) : base(exerciseService, dialogService)
        {
            OkCommand = new RelayCommand(() => Save(), () => true);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        private void Cancel()
        {
            Messenger.Default.Send(new ExerciseSelectionCancelledMessage());
        }

        private void Save()
        {
            if (SelectedExercise != null)
                Messenger.Default.Send(new ExerciseSelectedMessage(SelectedExercise.Id));
            else
                Messenger.Default.Send(new ExerciseSelectedMessage(0));
        }
    }
}
