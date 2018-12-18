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

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineEditViewModel : ValidatableViewModel
    {
        private IDialogViewService dialogService;

        public PracticeRoutine PracticeRoutine { get; private set; }

        public int Id { get; set; }

        private string title;
        [Required]
        public string Title
        {
            get { return title; }
            set
            {
                Set(() => Title, ref title, value, true, true);
            }
        }

        public int TotalMinutes
        {
            get { return 60; }
        }

        private EntityLifeCycleState lifeCycleState;
        public EntityLifeCycleState LifeCycleState
        {
            get
            {
                return lifeCycleState;
            }
            set
            {
                Set(() => LifeCycleState, ref lifeCycleState, value, false, false);
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public PracticeRoutineEditViewModel(IDialogViewService dialogService)
        {
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !this.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public void StartEdit(PracticeRoutine practiceRoutine)
        {
            LifeCycleState = practiceRoutine.Id > 0 ? EntityLifeCycleState.AsExistingEntity : EntityLifeCycleState.AsNewEntity;
            PracticeRoutine = practiceRoutine;

            if (PracticeRoutine != null) this.ErrorsChanged -= PracticeRoutine_ErrorsChanged;

            this.Revert();

            TrackChanges = true;

            this.ErrorsChanged += PracticeRoutine_ErrorsChanged;
        }

        private void Save()
        {
            this.Commit();
            Messenger.Default.Send(new EndEditingPracticeRoutineMessage(PracticeRoutine, EditorCloseOperation.Saved, 
                LifeCycleState));
        }

        private void Cancel()
        {
            this.Revert();
            Messenger.Default.Send(new EndEditingPracticeRoutineMessage(PracticeRoutine, EditorCloseOperation.Canceled, 
                LifeCycleState));
        }

        public override void Commit()
        {
            Mapper.Map(this, PracticeRoutine);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(PracticeRoutine, this);
            base.Revert();
        }

        private void PracticeRoutine_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}
