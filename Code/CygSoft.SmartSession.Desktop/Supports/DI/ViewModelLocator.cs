
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Exercises.Selection;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Edit;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Management;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Desktop.Supports.Factories;

namespace CygSoft.SmartSession.Desktop.Supports.DI
{
    /// <summary>
    /// This is the composition root...
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ExerciseEditViewModel ExerciseEditViewModel { get => Bootstrapper.Container.Resolve<ExerciseEditViewModel>(); }
        public ExerciseCompositeViewModel ExerciseCompositeViewModel { get => Bootstrapper.Container.Resolve<ExerciseCompositeViewModel>(); }
        public ExerciseManagementViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseManagementViewModel>(); }
        public ExerciseSelectionViewModel ExerciseSelectionViewModel { get => Bootstrapper.Container.Resolve<ExerciseSelectionViewModel>(); }

        public PracticeRoutineEditViewModel PracticeRoutineEditViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineEditViewModel>(); }
        public PracticeRoutineCompositeViewModel PracticeRoutineCompositeViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineCompositeViewModel>(); }
        public PracticeRoutineManagementViewModel PracticeRoutineManagementViewModel { get => Bootstrapper.Container.Resolve<PracticeRoutineManagementViewModel>(); }
        public RoutineRecorderViewModel RoutineRecorderViewModel { get => Bootstrapper.Container.Resolve<RoutineRecorderViewModel>(); }

        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }

        public IComponentFactory ComponentFactory { get => Bootstrapper.Container.Resolve<IComponentFactory>(); }
        public IViewModelFactory ViewModelFactory { get => Bootstrapper.Container.Resolve<IViewModelFactory>(); }
    }
}