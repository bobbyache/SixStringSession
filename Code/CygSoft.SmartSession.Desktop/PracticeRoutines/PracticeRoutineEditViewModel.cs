using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.Infrastructure.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineEditViewModel : ValidatableViewModel
    {
        private IDialogViewService dialogService;

        private PracticeRoutine practiceRoutine;

        private EntityLifeCycleState lifeCycleState;
        public EntityLifeCycleState LifeCycleState
        {
            get { return lifeCycleState; }
            set { Set(() => LifeCycleState, ref lifeCycleState, value, false, false); }
        }

        public int Id { get { return practiceRoutine.Id; } }

        [Required]
        public string Title
        {
            get { return practiceRoutine.Title; }
            set
            {
                practiceRoutine.Title = value;
                RaisePropertyChanged(() => Title);
                IsDirty = true;
            }
        }

        public string TotalTimeDisplay
        {
            get
            {
                return TimeFuncs.DisplayTimeFromSeconds(practiceRoutine.Sum(ts => ts.AssignedSeconds));
            }
        }

        private PracticeRoutineTreeViewModel practiceRoutineTree;
        public PracticeRoutineTreeViewModel PracticeRoutineTree
        {
            get { return practiceRoutineTree; }
            set
            {
                if (practiceRoutineTree != null)
                    practiceRoutineTree.PropertyChanged -= TreeModelViewChanged;

                Set(() => PracticeRoutineTree, ref practiceRoutineTree, value, false, false);

                practiceRoutineTree.PropertyChanged += TreeModelViewChanged;
            }
        }

        private void TreeModelViewChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => TotalTimeDisplay);
        }

        public PracticeRoutineEditViewModel(IDialogViewService dialogService)
        {
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");
        }

        public void StartEdit(PracticeRoutine practiceRoutine)
        {
            this.practiceRoutine = practiceRoutine;
            PracticeRoutineTree = new PracticeRoutineTreeViewModel(practiceRoutine);
            LifeCycleState = practiceRoutine.Id > 0 ? EntityLifeCycleState.AsExistingEntity : EntityLifeCycleState.AsNewEntity;
        }
    }
}
