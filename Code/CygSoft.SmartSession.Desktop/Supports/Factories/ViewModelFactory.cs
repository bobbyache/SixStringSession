using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Domain.Recording;

namespace CygSoft.SmartSession.Desktop.Supports.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private IViewModelFactory componentFactory;

        public ViewModelFactory(IViewModelFactory componentFactory)
        {
            this.componentFactory = componentFactory;
        }
        public ExerciseRecorderViewModel CreateRecorderViewModel(IExerciseRecorder exerciseRecorder)
        {
            var recorderViewModel = componentFactory.CreateRecorderViewModel(exerciseRecorder);
            componentFactory.Release(recorderViewModel);

            return recorderViewModel;
        }

        public void Release(ExerciseRecorderViewModel recorderViewModel)
        {
            this.componentFactory.Release(recorderViewModel);
        }
    }
}
