using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CygSoft.SmartSession.Dal.MySql;
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
            container.Register(Component.For<IDialogViewService>().ImplementedBy(typeof(DialogService)));

            container.Register(Component.For<IUnitOfWork>().ImplementedBy(typeof(UnitOfWork))
                .DependsOn(Dependency.OnConfigValue("connectionString", Settings.ConnectionString)).LifestyleSingleton());


            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<IComponentFactory>().AsFactory(),
                Component.For<IRecorder>().ImplementedBy<Recorder>().LifeStyle.Transient,
                Component.For<ISpeedProgress>().ImplementedBy<SpeedProgress>().LifeStyle.Transient,
                Component.For<IManualProgress>().ImplementedBy<ManualProgress>().LifeStyle.Transient,
                Component.For<IPracticeTimeProgress>().ImplementedBy<PracticeTimeProgress>().LifeStyle.Transient,
                Component.For<IExerciseRecorder>().ImplementedBy<ExerciseRecorder>().LifeStyle.Transient,
                Component.For<RecorderViewModel>().LifeStyle.Transient
                );

            container.Register(
                Component.For<IViewModelFactory>().ImplementedBy<ViewModelFactory>()
                );


            container.Register(Component.For<IExerciseService>().ImplementedBy(typeof(ExerciseService)));

            //container.Register(Component.For<IRecorder>().ImplementedBy(typeof(Recorder)));

            //container.Register(Component.For<IExerciseRecorder>().ImplementedBy(typeof(ExerciseRecorder)));
            //container.Register(Component.For<RecorderViewModel>());
            container.Register(Component.For<ExerciseEditViewModel>());
            container.Register(Component.For<ExerciseManagementViewModel>());
            container.Register(Component.For<ExerciseSelectionViewModel>());
            container.Register(Component.For<ExerciseCompositeViewModel>());
            container.Register(Component.For<SingleExerciseRecorderViewModel>());

            container.Register(Component.For<IPracticeRoutineService>().ImplementedBy(typeof(PracticeRoutineService)));
            container.Register(Component.For<PracticeRoutineEditViewModel>());
            container.Register(Component.For<PracticeRoutineManagementViewModel>());
            container.Register(Component.For<PracticeRoutineCompositeViewModel>());
            container.Register(Component.For<RoutineRecorderViewModel>());
            
            container.Register(Component.For<MainWindowViewModel>());

            //container.Register(Component.For<TimeSlotRecorderViewModel>().DependsOn(Onpar)
        }
    }
}