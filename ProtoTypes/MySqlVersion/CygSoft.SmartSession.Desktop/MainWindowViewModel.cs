using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.Supports;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CygSoft.SmartSession.Desktop
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISettings settings;
        private readonly ExerciseCompositeViewModel exerciseSearchViewModel;
        private readonly PracticeRoutineCompositeViewModel practiceRoutineSearchViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public string WindowTitle
        {
            get { return $"{settings.AssemblyTitle} [{settings.AssemblyVersion}]"; }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public MainWindowViewModel(
            ISettings settings,
            ExerciseCompositeViewModel exerciseCompositeViewModel, 
            PracticeRoutineCompositeViewModel practiceRoutineSearchViewModel)
        {
            this.settings = settings ?? throw new ArgumentNullException("ISettings must be provided.");
            this.exerciseSearchViewModel = exerciseCompositeViewModel ?? throw new ArgumentException("ExerciseCompositeViewModel must be provided.");
            this.practiceRoutineSearchViewModel = practiceRoutineSearchViewModel ?? throw new ArgumentException("PracticeRoutineSearchViewModel must be provided.");

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("ExerciseSearch");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "ExerciseSearch":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "PracticeRoutineSearch":
                    CurrentViewModel = practiceRoutineSearchViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
