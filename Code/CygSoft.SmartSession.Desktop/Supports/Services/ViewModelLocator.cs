
using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.GoalTasks;
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
        public ExerciseEditViewModel ExerciseEditViewModel { get => Bootstrapper.Container.Resolve<ExerciseEditViewModel>(); }
        public ExerciseCompositeViewModel ExerciseCompositeViewModel { get => Bootstrapper.Container.Resolve<ExerciseCompositeViewModel>(); }
        public ExerciseSearchViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchViewModel>(); }
        public ExerciseSearchCriteriaViewModel ExerciseSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchCriteriaViewModel>(); }

        public FileAttachmentEditViewModel FileAttachmentEditViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentEditViewModel>(); }
        public FileAttachmentCompositeViewModel FileAttachmentCompositeViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentCompositeViewModel>(); }
        public FileAttachmentSearchViewModel FileAttachmentSearchViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentSearchViewModel>(); }
        public FileAttachmentSearchCriteriaViewModel FileAttachmentSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentSearchCriteriaViewModel>(); }

        public GoalEditViewModel GoalEditViewModel { get => Bootstrapper.Container.Resolve<GoalEditViewModel>(); }
        public GoalCompositeViewModel GoalCompositeViewModel { get => Bootstrapper.Container.Resolve<GoalCompositeViewModel>(); }
        public GoalSearchViewModel GoalSearchViewModel { get => Bootstrapper.Container.Resolve<GoalSearchViewModel>(); }
        public GoalSearchCriteriaViewModel GoalSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<GoalSearchCriteriaViewModel>(); }

        public GoalTaskEditViewModel GoalTaskEditViewModel { get => Bootstrapper.Container.Resolve<GoalTaskEditViewModel>(); }
        public GoalTaskCompositeViewModel GoalTaskCompositeViewModel { get => Bootstrapper.Container.Resolve<GoalTaskCompositeViewModel>(); }
        public GoalTaskSearchViewModel GoalTaskSearchViewModel { get => Bootstrapper.Container.Resolve<GoalTaskSearchViewModel>(); }
        public GoalTaskSearchCriteriaViewModel GoalTaskSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<GoalTaskSearchCriteriaViewModel>(); }


        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }
        public TaskSearchViewModel TaskSearchViewModel { get => Bootstrapper.Container.Resolve<TaskSearchViewModel>(); }
    }
}