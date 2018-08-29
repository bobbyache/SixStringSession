using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Desktop.Tasks;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.EF;
using CygSoft.SmartSession.EF.Repositories;
using GalaSoft.MvvmLight.Views;

namespace CygSoft.SmartSession.Desktop.Supports.DI
{
    // https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
    // http://tommarien.github.io/blog/2012/04/22/castle-windsor-how-to-register-components
    // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7

    public class Installers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SmartSessionContext>().LifestyleSingleton());
            container.Register(Component.For<IDialogService>().ImplementedBy(typeof(DialogService)));
            container.Register(Component.For<IExerciseRepository>().ImplementedBy(typeof(ExerciseRepository)));
            container.Register(Component.For<IExerciseService>().ImplementedBy(typeof(ExerciseService)));

            container.Register(Component.For<ExerciseSearchViewModel>());
            container.Register(Component.For<TaskSearchViewModel>());
            container.Register(Component.For<GoalSearchViewModel>());
            container.Register(Component.For<MainWindowViewModel>());

            container.Register(Component.For<GoalListViewModel>());
        }
    }
}