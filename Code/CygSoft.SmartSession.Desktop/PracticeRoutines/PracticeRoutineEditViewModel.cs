using AutoMapper;
using CygSoft.SmartSession.Desktop.PracticeRoutines.PracticeRoutineTree;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Infrastructure;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand AddTimeSlotCommand { get; private set; }
        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public BindingList<TimeSlotViewModel> TimeSlots { get; } = new BindingList<TimeSlotViewModel>();

        public RelayCommand<ViewModelBase> SelectedItemChangedCommand { get; private set; }

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

        private ViewModelBase selectedItem;
        public ViewModelBase SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                Set(() => SelectedItem, ref selectedItem, value);
            }
        }

        public string TotalTimeDisplay
        {
            get
            {
                return TimeFuncs.DisplayTimeFromSeconds(TimeSlots.Sum(ts => ts.AssignedSeconds));
            }
        }

        private void TreeModelViewChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => TotalTimeDisplay);
        }

        public PracticeRoutineEditViewModel(IDialogViewService dialogService)
        {
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !this.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
            AddTimeSlotCommand = new RelayCommand(() => AddTimeSlot(), () => true);
            AddExerciseCommand = new RelayCommand(() => AddExercise(), () => true);
            DeleteCommand = new RelayCommand(() => Delete(), () => true);

            SelectedItemChangedCommand = new RelayCommand<ViewModelBase>(c => SelectedItem = c);
        }

        public void StartEdit(PracticeRoutine practiceRoutine)
        {
            this.practiceRoutine = practiceRoutine ?? throw new ArgumentNullException("PracticeRoutine must be provided.");
            LifeCycleState = practiceRoutine.Id > 0 ? EntityLifeCycleState.AsExistingEntity : EntityLifeCycleState.AsNewEntity;

            if (TimeSlots != null)
            {
                TimeSlots.ListChanged -= (s, e) => RaisePropertyChanged(() => TotalTimeDisplay);
                TimeSlots.Clear();
            }

            foreach (var timeSlot in practiceRoutine)
            {
                TimeSlotViewModel timeSlotViewModel = new TimeSlotViewModel(timeSlot);
                TimeSlots.Add(timeSlotViewModel);
            }

            TimeSlots.ListChanged += (s, e) => RaisePropertyChanged(() => TotalTimeDisplay);
        }

        private void AddTimeSlot()
        {
            PracticeRoutineTimeSlot timeSlot = new PracticeRoutineTimeSlot("New Time Slot", 300, new List<TimeSlotExercise>());
            practiceRoutine.Add(timeSlot);
            TimeSlots.Add(new TimeSlotViewModel(timeSlot));
        }

        private void AddExercise()
        {
            Messenger.Default.Send(new StartSelectingPracticeRoutineExerciseMessage());
        }
        public void AddTimeSlotExercise(TimeSlotExercise timeSlotExercise)
        {
            if (SelectedItem is TimeSlotViewModel)
            {
                var timeSlotViewModel = SelectedItem as TimeSlotViewModel;
                TimeSlotExerciseViewModel timeSlotExerciseViewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
                timeSlotViewModel.TimeSlot.Add(timeSlotExercise);
                timeSlotViewModel.Exercises.Add(timeSlotExerciseViewModel);
            }
            else
            {
                TimeSlotExerciseViewModel selectedExerciseViewModel = SelectedItem as TimeSlotExerciseViewModel;
                var timeSlotViewModel = selectedExerciseViewModel.TimeSlotViewModel;
                TimeSlotExerciseViewModel timeSlotExerciseViewModel = new TimeSlotExerciseViewModel(timeSlotExercise, timeSlotViewModel);
                timeSlotViewModel.TimeSlot.Add(timeSlotExercise);
                timeSlotViewModel.Exercises.Add(timeSlotExerciseViewModel);
            }
        }

        private void Delete()
        {
            if (SelectedItem is TimeSlotViewModel)
            {
                practiceRoutine.Remove((SelectedItem as TimeSlotViewModel).TimeSlot);
                TimeSlots.Remove(SelectedItem as TimeSlotViewModel);
            }
            else
            {
                var exerciseViewModel = SelectedItem as TimeSlotExerciseViewModel;
                var timeSlotViewModel = exerciseViewModel.TimeSlotViewModel;

                timeSlotViewModel.TimeSlot.Remove(exerciseViewModel.TimeSlotExercise);
                timeSlotViewModel.Remove(exerciseViewModel);
            }
        }

        private void Save()
        {
            this.Commit();
            Messenger.Default.Send(new EndEditingPracticeRoutineMessage(practiceRoutine, EditorCloseOperation.Saved,
                LifeCycleState));
        }

        private void Cancel()
        {
            this.Revert();
            Messenger.Default.Send(new EndEditingPracticeRoutineMessage(practiceRoutine, EditorCloseOperation.Canceled,
                LifeCycleState));
        }

        public override void Commit()
        {
            Mapper.Map(this, practiceRoutine);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(practiceRoutine, this);
            base.Revert();
        }
    }
}
