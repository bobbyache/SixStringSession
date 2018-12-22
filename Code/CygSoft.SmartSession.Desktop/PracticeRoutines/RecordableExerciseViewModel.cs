using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Sessions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.PracticeRoutines
{
    public class RecordableExerciseViewModel : ViewModelBase
    {
        private Exercise exercise;
        private IExerciseRecorder exerciseRecorder;

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

        private bool recording;
        public bool Recording
        {
            get
            {
                return recording;
            }
            set
            {
                Set(() => Recording, ref recording, value);
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(() => Title, ref title, value);
            }
        }

        public RecordableExerciseViewModel(Exercise exercise)
        {
            this.exercise = exercise;
            exerciseRecorder = new ExerciseRecorder();
            exerciseRecorder.TickActionCallBack = TickTock;
            exerciseRecorder.RecordingStatusChanged += ExerciseRecorder_RecordingStatusChanged;

            Title = exercise.Title;
            Recording = exerciseRecorder.Recording;
            DisplayTime = exerciseRecorder.DisplayTime;
        }

        private void ExerciseRecorder_RecordingStatusChanged(object sender, EventArgs e)
        {
            recording = exerciseRecorder.Recording;
        }

        private void TickTock()
        {
            displayTime = exerciseRecorder.DisplayTime;
        }

        public void Start()
        {
            if (!exerciseRecorder.Recording)
            {
                exerciseRecorder.Resume();
            }
        }

        internal void PauseRecording()
        {
            if (exerciseRecorder.Recording)
            {
                exerciseRecorder.Pause();
            }
        }

        private void CancelRecording()
        {
            exerciseRecorder.Reset();
        }
    }
}
