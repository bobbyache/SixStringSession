using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;

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

        private PracticeRoutineTreeViewModel practiceRoutineTree;
        public PracticeRoutineTreeViewModel PracticeRoutineTree
        {
            get { return practiceRoutineTree; }
            set { Set(() => PracticeRoutineTree, ref practiceRoutineTree, value, false, false); }
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
