/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmLight_Prototypes"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Castle.Windsor;
using Castle.Windsor.Installer;
using CommonServiceLocator;
using CygSoft.SmartSession.Desktop.DI;
using GalaSoft.MvvmLight.Ioc;

namespace MvvmLight_Prototypes.ViewModel
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