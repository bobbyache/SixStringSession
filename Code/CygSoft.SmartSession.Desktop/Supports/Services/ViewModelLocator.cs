
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Exercises.Selection;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.Supports.DI;

namespace CygSoft.SmartSession.Desktop.Supports.Services
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ExerciseEditViewModel ExerciseEditViewModel { get => Bootstrapper.Container.Resolve<ExerciseEditViewModel>(); }
        public ExerciseCompositeViewModel ExerciseCompositeViewModel { get => Bootstrapper.Container.Resolve<ExerciseCompositeViewModel>(); }
        public ExerciseManagementViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseManagementViewModel>(); }
        public ExerciseSelectionViewModel ExerciseSelectionViewModel { get => Bootstrapper.Container.Resolve<ExerciseSelectionViewModel>(); }
        public SingleExerciseRecorderViewModel ExerciseRecorderViewModel { get => Bootstrapper.Container.Resolve<SingleExerciseRecorderViewModel>(); }
                
        public PracticeRoutineEditViewModel PracticeRoutineEditViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineEditViewModel>(); }
        public PracticeRoutineCompositeViewModel PracticeRoutineCompositeViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineCompositeViewModel>(); }
        public PracticeRoutineManagementViewModel PracticeRoutineManagementViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineManagementViewModel>(); }
        public PracticeRoutineRecordingListViewModel PracticeRoutineRecordingListViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineRecordingListViewModel>(); }

        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }
    }
}