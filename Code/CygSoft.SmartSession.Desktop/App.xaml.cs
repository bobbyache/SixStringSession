using AutoMapper;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Domain.Exercises;
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
                cfg.CreateMap<ExerciseModel, Exercise>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Exercise, Exercise>();
                cfg.CreateMap<Exercise, ExerciseSearchResult>();
                cfg.CreateMap<ExerciseSearchResult, Exercise>();
                cfg.CreateMap<ExerciseSearchResult, ExerciseSearchResult>();
                cfg.CreateMap<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>();
            });
        }
    }
}
