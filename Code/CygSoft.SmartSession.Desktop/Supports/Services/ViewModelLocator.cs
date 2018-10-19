
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
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
        public ExerciseSearchViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchViewModel>(); }
        public ExerciseSearchCriteriaViewModel ExerciseSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchCriteriaViewModel>(); }
        public ExerciseRecorderViewModel ExerciseRecorderViewModel { get => Bootstrapper.Container.Resolve<ExerciseRecorderViewModel>(); }

        public GoalEditViewModel GoalEditViewModel { get => Bootstrapper.Container.Resolve<GoalEditViewModel>(); }
        public GoalCompositeViewModel GoalCompositeViewModel { get => Bootstrapper.Container.Resolve<GoalCompositeViewModel>(); }
        public GoalSearchViewModel GoalSearchViewModel { get => Bootstrapper.Container.Resolve<GoalSearchViewModel>(); }
        public GoalSearchCriteriaViewModel GoalSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<GoalSearchCriteriaViewModel>(); }

        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }
    }
}