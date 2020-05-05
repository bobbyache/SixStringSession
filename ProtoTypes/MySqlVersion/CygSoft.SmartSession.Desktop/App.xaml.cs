using AutoMapper;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Exercises.Edit;
using CygSoft.SmartSession.Desktop.Exercises.Management;
using CygSoft.SmartSession.Desktop.PracticeRoutines;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Edit;
using CygSoft.SmartSession.Desktop.PracticeRoutines.Management;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.PracticeRoutines;
using System.Windows;

namespace CygSoft.SmartSession.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ExerciseEditViewModel, Exercise>();
                cfg.CreateMap<Exercise, ExerciseEditViewModel>();
                cfg.CreateMap<Exercise, Exercise>();
                cfg.CreateMap<Exercise, ExerciseListItemModel>();
                cfg.CreateMap<ExerciseListItemModel, Exercise>();
                cfg.CreateMap<ExerciseListItemModel, ExerciseListItemModel>();

                cfg.CreateMap<PracticeRoutineEditViewModel, PracticeRoutine>();
                cfg.CreateMap<PracticeRoutine, PracticeRoutineEditViewModel>();
                cfg.CreateMap<PracticeRoutine, PracticeRoutine>();

                cfg.CreateMap<PracticeRoutineHeader, PracticeRoutineListItemModel>();
                cfg.CreateMap<PracticeRoutineListItemModel, PracticeRoutineHeader>();

                cfg.CreateMap<PracticeRoutineListItemModel, PracticeRoutineListItemModel>();
            });
        }
    }
}
