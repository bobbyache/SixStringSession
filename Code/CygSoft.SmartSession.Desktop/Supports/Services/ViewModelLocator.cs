
using CygSoft.SmartSession.Desktop.Attachments;
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
        public ExerciseEditViewModel ExerciseEditViewModel { get => Bootstrapper.Container.Resolve<ExerciseEditViewModel>(); }
        public ExerciseCompositeViewModel ExerciseCompositeViewModel { get => Bootstrapper.Container.Resolve<ExerciseCompositeViewModel>(); }
        public ExerciseSearchViewModel ExerciseSearchViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchViewModel>(); }
        public ExerciseSearchCriteriaViewModel ExerciseSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<ExerciseSearchCriteriaViewModel>(); }

        public FileAttachmentEditViewModel FileAttachmentEditViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentEditViewModel>(); }
        public FileAttachmentCompositeViewModel FileAttachmentCompositeViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentCompositeViewModel>(); }
        public FileAttachmentSearchViewModel FileAttachmentSearchViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentSearchViewModel>(); }
        public FileAttachmentSearchCriteriaViewModel FileAttachmentSearchCriteriaViewModel { get => Bootstrapper.Container.Resolve<FileAttachmentSearchCriteriaViewModel>(); }

        public MainWindowViewModel MainWindowViewModel { get => Bootstrapper.Container.Resolve<MainWindowViewModel>(); }
        public GoalSearchViewModel GoalSearchViewModel { get => Bootstrapper.Container.Resolve<GoalSearchViewModel>(); }
        public TaskSearchViewModel TaskSearchViewModel { get => Bootstrapper.Container.Resolve<TaskSearchViewModel>(); }
    }
}