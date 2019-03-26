using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CygSoft.SmartSession.Dal.MySql;
using CygSoft.SmartSession.Dal.MySql.Common;
using CygSoft.SmartSession.Dal.MySql.Repositories;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.Exercises.Recorder;
using CygSoft.SmartSession.Desktop.Exercises.Selection;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Edit;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Management;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Recorder;
using CygSoft.SmartSession.Desktop.Supports.Factories;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Recording;
using System.Data;

namespace CygSoft.SmartSession.Desktop.Supports.DI
{
    // https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
    // http://tommarien.github.io/blog/2012/04/22/castle-windsor-how-to-register-components
    // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7
    // https://stackoverflow.com/questions/27507396/how-to-constructor-inject-a-string-that-is-only-known-at-runtime-windsor-castl


    /*
    --------------------------------------------------------------------------------------------------------------------------------
    TODO: (Requires own branch) Create a factory for the creation of all your new objects.
    ****************************************
    Create an Object Factory through
    Castle Windsor...
    ****************************************

    Specific Documentation: Typed Factory Facility - interface-based factories
    https://github.com/castleproject/Windsor/blob/master/docs/typed-factory-facility-interface-based.md

        originally sourced:
            https://stackoverflow.com/questions/11726663/passing-part-of-constructor-parameters-to-castle-windsor-container/11734671

    You should prefer Typed Factory instead of using container like service locator. Just define factory interface:

    public interface IFooFactory {
        IFoo Create(int somenumber);
    }

    container.Register(Component.For<IFooFactory>().AsFactory());

    var foo = fooFactory.Create(desiredArgumentValue);
    --------------------------------------------------------------------------------------------------------------------------------

        // Composition Root Testing:
        // https://programming.lansky.name/composition-root-testing
    */

    public class Installers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ISettings settings = Settings.AppSettings;
            container.Register(Component.For<ISettings>().Instance(settings).LifeStyle.Singleton);

            //IConnectionManager connectionManager = new ConnectionManager(settings.ConnectionString);
            //container.Register(Component.For<IDbConnection>().Instance(
            //    .DependsOn(Dependency.OnConfigValue("connectionString", Settings.ConnectionString)).LifeStyle.Singleton);
            container.Register(Component.For<IDbConnection>().Instance(settings.DatabaseConnection).LifeStyle.Singleton);

            container.Register(Component.For<IDialogViewService>().ImplementedBy(typeof(DialogService)));

            container.Register(Component.For<IExerciseRepository>().ImplementedBy<ExerciseRepository>().LifeStyle.Singleton);
            container.Register(Component.For<IPracticeRoutineRepository>().ImplementedBy<PracticeRoutineRepository>().LifeStyle.Singleton);
            container.Register(Component.For<IUnitOfWork>().ImplementedBy(typeof(UnitOfWork)).LifestyleSingleton());

            container.AddFacility<TypedFactoryFacility>();
            container.Register(
                Component.For<IViewModelFactory>().AsFactory(),
                Component.For<IRecorder>().ImplementedBy<Recorder>().LifeStyle.Transient,
                Component.For<ISpeedProgress>().ImplementedBy<SpeedProgress>().LifeStyle.Transient,
                Component.For<IManualProgress>().ImplementedBy<ManualProgress>().LifeStyle.Transient,
                Component.For<IPracticeTimeProgress>().ImplementedBy<PracticeTimeProgress>().LifeStyle.Transient,
                Component.For<IExerciseRecorder>().ImplementedBy<ExerciseRecorder>().LifeStyle.Transient,
                Component.For<ExerciseRecorderViewModel>().LifeStyle.Transient
                );

            container.Register(
                Component.For<IViewModelFactory>().ImplementedBy<ViewModelFactory>()
                );


            container.Register(Component.For<IExerciseService>().ImplementedBy(typeof(ExerciseService)));
            container.Register(Component.For<ExerciseEditViewModel>());
            container.Register(Component.For<ExerciseManagementViewModel>());
            container.Register(Component.For<ExerciseSelectionViewModel>());
            container.Register(Component.For<ExerciseCompositeViewModel>());

            container.Register(Component.For<IPracticeRoutineService>().ImplementedBy(typeof(PracticeRoutineService)));
            container.Register(Component.For<PracticeRoutineEditViewModel>());
            container.Register(Component.For<PracticeRoutineManagementViewModel>());
            container.Register(Component.For<PracticeRoutineCompositeViewModel>());
            container.Register(Component.For<RoutineRecorderViewModel>());
            
            container.Register(Component.For<MainWindowViewModel>());
        }
    }
}