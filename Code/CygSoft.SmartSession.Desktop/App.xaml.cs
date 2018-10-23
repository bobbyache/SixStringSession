using AutoMapper;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
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
                cfg.CreateMap<Exercise, ExerciseSearchResultModel>();
                cfg.CreateMap<ExerciseSearchResultModel, Exercise>();
                cfg.CreateMap<ExerciseSearchResultModel, ExerciseSearchResultModel>();
                cfg.CreateMap<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>();

                cfg.CreateMap<GoalModel, Goal>();
                cfg.CreateMap<Goal, GoalModel>();
                cfg.CreateMap<Goal, Goal>();
                cfg.CreateMap<Goal, GoalSearchResultModel>();
                cfg.CreateMap<GoalSearchResultModel, Goal>();
                cfg.CreateMap<GoalSearchResultModel, GoalSearchResultModel>();
                cfg.CreateMap<GoalSearchCriteriaViewModel, GoalSearchCriteria>();
            });
        }
    }
}
