﻿using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Supports.Validators;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CygSoft.SmartSession.Desktop.Exercises.Edit
{
    public class ExerciseEditViewModel : ValidatableViewModel
    {
        private IDialogViewService dialogService;

        public IExercise Exercise { get; private set; }

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

        private int? targetMetronomeSpeed;
        [Range(0, 250, ErrorMessage = "Value must be between 0 and 250.")]
        public int? TargetMetronomeSpeed
        {
            get { return targetMetronomeSpeed; }
            set
            {
                Set(() => TargetMetronomeSpeed, ref targetMetronomeSpeed, value, true, true);
            }
        }

        private int speedProgressWeighting;
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int SpeedProgressWeighting
        {
            get { return speedProgressWeighting; }
            set
            {
                Set(() => SpeedProgressWeighting, ref speedProgressWeighting, value, true, true);
            }
        }

        private int practiceTimeProgressWeighting;
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int PracticeTimeProgressWeighting
        {
            get { return practiceTimeProgressWeighting; }
            set
            {
                Set(() => PracticeTimeProgressWeighting, ref practiceTimeProgressWeighting, value, true, true);
            }
        }

        private int manualProgressWeighting;
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public int ManualProgressWeighting
        {
            get { return manualProgressWeighting; }
            set
            {
                Set(() => ManualProgressWeighting, ref manualProgressWeighting, value, true, true);
            }
        }

        private int? targetPracticeTime;
        [Range(0, 1000000000, ErrorMessage = "Value must be between 0 and 1,000,000,000.")]
        public int? TargetPracticeTime
        {
            get { return targetPracticeTime; }
            set
            {
                Set(() => TargetPracticeTime, ref targetPracticeTime, value, true, true);
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

        public override void Commit()
        {
            Mapper.Map(this, Exercise);
            base.Commit();
        }

        public override void Revert()
        {
            Mapper.Map(Exercise, this);
            base.Revert();
        }

        public ExerciseEditViewModel(IDialogViewService dialogService)
        {
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !this.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
            DeleteFilesCommand = new RelayCommand(() => DeleteFiles(), () => true);
            OpenFileCommand = new RelayCommand(() => OpenFile(), () => true);
        }

        private void OpenFile()
        {
            //exerciseService.OpenFile(Exercise.Id, null);
        }

        public void DeleteFiles()
        {
            //exerciseService.DeleteFiles(Exercise.Id);
        }


        public void AddFiles(string[] files)
        {
            //exerciseService.AddFiles(Exercise.Id, files);
        }

        public void StartEdit(IExercise exercise)
        {
            LifeCycleState = exercise.Id > 0 ? EntityLifeCycleState.AsExistingEntity : EntityLifeCycleState.AsNewEntity;
            Exercise = exercise;

            if (Exercise != null) this.ErrorsChanged -= Exercise_ErrorsChanged;

            this.Revert();

            TrackChanges = true;

            this.ErrorsChanged += Exercise_ErrorsChanged;
        }

        private void Exercise_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            this.Commit();
            Messenger.Default.Send(new EndEditingExerciseMessage(Exercise, EditorCloseOperation.Saved, LifeCycleState));
        }

        private void Cancel()
        {
            this.Revert();
            Messenger.Default.Send(new EndEditingExerciseMessage(Exercise, EditorCloseOperation.Canceled, LifeCycleState));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public RelayCommand DeleteFilesCommand { get; private set; }

        public RelayCommand OpenFileCommand { get; private set; }
    }
}
