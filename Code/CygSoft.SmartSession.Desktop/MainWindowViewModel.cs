using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.GoalTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CygSoft.SmartSession.Desktop
{
    // Multiple Views and View Models: Opening Dialogs.... https://www.youtube.com/watch?v=Dzv8CtUCchY

    public class MainWindowViewModel : ViewModelBase
    {
        private ExerciseCompositeViewModel exerciseSearchViewModel;
        private GoalCompositeViewModel goalSearchViewModel;
        private GoalTaskCompositeViewModel goalTaskSearchViewModel;
        private FileAttachmentCompositeViewModel fileAttachmentViewModel;


        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public MainWindowViewModel(ExerciseCompositeViewModel exerciseSearchViewModel, FileAttachmentCompositeViewModel fileAttachmentViewModel, 
            GoalCompositeViewModel goalSearchViewModel, GoalTaskCompositeViewModel goalTaskSearchViewModel)
        {
            this.exerciseSearchViewModel = exerciseSearchViewModel;
            this.fileAttachmentViewModel = fileAttachmentViewModel;
            this.goalSearchViewModel = goalSearchViewModel;
            this.goalTaskSearchViewModel = goalTaskSearchViewModel;

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("ExerciseSearch");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "GoalSearch":
                    CurrentViewModel = goalSearchViewModel;
                    break;
                case "GoalTaskSearch":
                    CurrentViewModel = goalTaskSearchViewModel;
                    break;
                case "ExerciseSearch":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "FileAttachmentSearch":
                    CurrentViewModel = fileAttachmentViewModel;
                    break;
                default:
                    CurrentViewModel = fileAttachmentViewModel;
                    break;
            }
        }
    }
}
