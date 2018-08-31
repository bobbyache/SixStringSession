using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Exercises
{
    public class ExerciseCompositeViewModel : ViewModelBase
    {
        private ExerciseSearchViewModel exerciseSearchViewModel;
        private ExerciseEditViewModel exerciseEditViewModel;

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(() => CurrentViewModel, ref currentViewModel, value); }
        }

        public RelayCommand<string> NavigationCommand { get; private set; }

        public ExerciseCompositeViewModel(ExerciseSearchViewModel exerciseSearchViewModel, ExerciseEditViewModel exerciseEditViewModel)
        {
            this.exerciseSearchViewModel = exerciseSearchViewModel;
            this.exerciseEditViewModel = exerciseEditViewModel;

            NavigationCommand = new RelayCommand<string>(OnNavigation);
            OnNavigation("Search");
        }

        private void OnNavigation(string destination)
        {
            switch (destination)
            {
                case "Search":
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
                case "Edit":
                    CurrentViewModel = exerciseEditViewModel;
                    break;
                default:
                    CurrentViewModel = exerciseSearchViewModel;
                    break;
            }
        }
    }
}
