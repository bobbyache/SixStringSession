
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
        public GoalListViewModel Main { get => Bootstrapper.Container.Resolve<GoalListViewModel>(); }
        public ExerciseSearchViewModel ExerciseSearch { get => Bootstrapper.Container.Resolve<ExerciseSearchViewModel>(); }
    }
}