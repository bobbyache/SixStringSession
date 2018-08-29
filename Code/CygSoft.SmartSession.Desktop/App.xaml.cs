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

            Mapper.Initialize(cfg => cfg.CreateMap<Exercise, ExerciseSearchResult>());

        }
    }
}
