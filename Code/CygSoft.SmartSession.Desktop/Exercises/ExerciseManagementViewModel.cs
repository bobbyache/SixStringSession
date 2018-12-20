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
    public class ExerciseManagementViewModel : ExerciseSearchViewModel
    {
        public ExerciseManagementViewModel(ExerciseSearchCriteriaViewModel exerciseSearchCriteriaViewModel, IExerciseService exerciseService,
            IDialogViewService dialogService) : base(exerciseSearchCriteriaViewModel, exerciseService, dialogService)
        {
            RecordExerciseCommand = new RelayCommand(RecordExercise, () => SelectedExercise != null);
        }

        private void RecordExercise()
        {
            Messenger.Default.Send(new OpenExerciseRecorderMessage(SelectedExercise.Id));
        }

        public RelayCommand RecordExerciseCommand { get; private set; }
    }
}
