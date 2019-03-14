using System;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseManagementViewModel exerciseSearchViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;
        //private RecorderViewModel recorderViewModel;
        private IExerciseService exerciseService;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(IExerciseService exerciseService, ExerciseManagementViewModel exerciseManagementViewModel, 
            ExerciseEditViewModel exerciseEditViewModel //,
            //RecorderViewModel recorderViewModel
            )
        {
            this.exerciseService = exerciseService;
            this.exerciseSearchViewModel = exerciseManagementViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;
            //this.recorderViewModel = recorderViewModel;

            Messenger.Default.Register<StartEditingExerciseMessage>(this, (m) => StartEditingExercise(m.Exercise));
            Messenger.Default.Register<EndEditingExerciseMessage>(this, (m) => EndEditingExercise(m.Exercise, m.Operation, m.LifeCycleState));
            //Messenger.Default.Register<OpenExerciseRecorderMessage>(this, (m) => RecordExercise(m.ExerciseId));

            Messenger.Default.Register<CancelledExerciseRecordingMessage>(this, (m) => RecordingCancelled());
            Messenger.Default.Register<SavedExerciseRecordingMessage>(this, (m) => RecordingSaved());
            Messenger.Default.Register<StartPracticingExerciseMessage>(this, (m) => StartPracticingExercise(m.ExerciseId));

            NavigationCommand = new RelayCommand<string>(NavigateTo);
            NavigateTo("Search");
        }

        private void StartPracticingExercise(int exerciseId)
        {
            throw new NotImplementedException();
        }

        //private void RecordExercise(int exerciseId)
        //{
        //    var exerciseRecorder = exerciseService.GetExerciseRecorder(exerciseId);
        //    //new RecorderViewModel()
        //    //recorderViewModel.InitializeRecorder(exerciseRecorder);
        //    NavigateTo("Record");
        //}

        private void RecordingCancelled()
        {
            NavigateTo("Search");
        }

        private void RecordingSaved()
        {
            NavigateTo("Search");
        }

        protected virtual void EndEditingExercise(IExercise exercise, EditorCloseOperation operation, 
            EntityLifeCycleState entityLifeCycleState)
        {
            if (operation == EditorCloseOperation.Saved)
            {
                if (entityLifeCycleState == EntityLifeCycleState.AsExistingEntity)
                    exerciseService.Update(exercise);
                else
                    exerciseService.Add(exercise);
            }

            NavigateTo("Search");
        }


        private void StartEditingExercise(IExercise exercise)
        {
            exerciseEditViewModel.StartEdit(exercise);
            NavigateTo("Edit");
        }

        private void NavigateTo(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = exerciseEditViewModel;
                    break;
                //case "Record":
                //    CurrentViewModel = recorderViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
