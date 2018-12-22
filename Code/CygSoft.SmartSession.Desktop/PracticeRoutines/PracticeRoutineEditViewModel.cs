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

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class PracticeRoutineEditViewModel : ValidatableViewModel
    {
        private IDialogViewService dialogService;

        public PracticeRoutine PracticeRoutine
        {
            get;
            private set;
        }

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

        public BindingList<PracticeRoutineExercise> PracticeRoutineExercises
        {
            get;
            private set;
        } = new BindingList<PracticeRoutineExercise>();

        private PracticeRoutineExercise selectedPracticeRoutineExercise;
        public PracticeRoutineExercise SelectedPracticeRoutineExercise
        {
            get { return selectedPracticeRoutineExercise; }
            set
            {
                Set(() => SelectedPracticeRoutineExercise, ref selectedPracticeRoutineExercise, value, true, true);
            }
        }

        private string displayTime;
        public string DisplayTime
        {
            get
            {
                return displayTime;
            }
            set
            {
                Set(() => DisplayTime, ref displayTime, value);
            }
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

        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteExerciseCommand { get; private set; }

        public PracticeRoutineEditViewModel(IDialogViewService dialogService)
        {
            this.PracticeRoutineExercises.ListChanged += PracticeRoutineExercises_ListChanged;
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !this.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
            AddExerciseCommand = new RelayCommand(() => AddExercise(), () => true);
            DeleteExerciseCommand = new RelayCommand(() => DeleteExercise(), () => true);
        }

        private void PracticeRoutineExercises_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (PracticeRoutineExercises.Any())
                DisplayTime = $"Total Time: {TimeFuncs.DisplayTimeFromSeconds(PracticeRoutineExercises.Sum(p => p.Seconds))}";
            else
                DisplayTime = $"Total Time: {TimeFuncs.DisplayTimeFromSeconds(0)}";
        }

        public void AddPracticeRoutineExercise(PracticeRoutineExercise routineExercise)
        {
            PracticeRoutineExercises.Add(routineExercise);
        }

        private void AddExercise()
        {
            Messenger.Default.Send(new StartSelectingPracticeRoutineExerciseMessage());
        }

        private void DeleteExercise()
        {
            PracticeRoutineExercises.Remove(SelectedPracticeRoutineExercise);
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
