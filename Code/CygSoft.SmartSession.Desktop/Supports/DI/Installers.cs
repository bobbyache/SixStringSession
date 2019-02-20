using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CygSoft.SmartSession.Dal.MySql;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain;
using CygSoft.SmartSession.Domain.Common;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using CygSoft.SmartSession.Domain.Sessions;

namespace CygSoft.SmartSession.Desktop.Supports.DI
{
    // https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
    // http://tommarien.github.io/blog/2012/04/22/castle-windsor-how-to-register-components
    // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7
    // https://stackoverflow.com/questions/27507396/how-to-constructor-inject-a-string-that-is-only-known-at-runtime-windsor-castl

    public class Installers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDialogViewService>().ImplementedBy(typeof(DialogService)));

            container.Register(Component.For<IFileService>().ImplementedBy(typeof(FileService))
                .DependsOn(Dependency.OnConfigValue("folderPath", Settings.FileAttachmentFolder)).LifestyleSingleton());

            container.Register(Component.For<IUnitOfWork>().ImplementedBy(typeof(UnitOfWork))
                .DependsOn(Dependency.OnConfigValue("connectionString", Settings.ConnectionString)).LifestyleSingleton());

            container.Register(Component.For<IExerciseService>().ImplementedBy(typeof(ExerciseService)));

            container.Register(Component.For<IRecorder>().ImplementedBy(typeof(Recorder)));
            
            container.Register(Component.For<ExerciseEditViewModel>());
            container.Register(Component.For<ExerciseManagementViewModel>());
            container.Register(Component.For<ExerciseSelectionViewModel>());
            container.Register(Component.For<ExerciseCompositeViewModel>());
            container.Register(Component.For<ExerciseSearchCriteriaViewModel>());
            container.Register(Component.For<ExerciseRecorderViewModel>());

            container.Register(Component.For<IPracticeRoutineService>().ImplementedBy(typeof(PracticeRoutineService)));
            container.Register(Component.For<PracticeRoutineEditViewModel>());
            container.Register(Component.For<PracticeRoutineManagementViewModel>());
            container.Register(Component.For<PracticeRoutineCompositeViewModel>());
            container.Register(Component.For<PracticeRoutineRecordingListViewModel>());
            
        container.Register(Component.For<IGoalService>().ImplementedBy(typeof(GoalService)));
            container.Register(Component.For<GoalEditViewModel>());
            container.Register(Component.For<GoalSearchViewModel>());
            container.Register(Component.For<GoalCompositeViewModel>());
            container.Register(Component.For<GoalSearchCriteriaViewModel>());
            container.Register(Component.For<GoalListViewModel>());

            container.Register(Component.For<MainWindowViewModel>());
        }
    }
}