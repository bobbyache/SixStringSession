
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.Supports.DI;
using CygSoft.SmartSession.Desktop.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.Services
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }
        public ExerciseSearchViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchViewModel>(); }
        public GoalSearchViewModel GoalSearchViewModel { get => Bootstrapper.Container.Resolve<GoalSearchViewModel>(); }
        public TaskSearchViewModel TaskSearchViewModel { get => Bootstrapper.Container.Resolve<TaskSearchViewModel>(); }
    }
}