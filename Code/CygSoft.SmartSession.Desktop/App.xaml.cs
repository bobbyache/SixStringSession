using AutoMapper;
using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Domain.Attachments;
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
                cfg.CreateMap<ExerciseModel, Exercise>();
                cfg.CreateMap<Exercise, ExerciseModel>();
                cfg.CreateMap<Exercise, Exercise>();
                cfg.CreateMap<Exercise, ExerciseSearchResult>();
                cfg.CreateMap<ExerciseSearchResult, Exercise>();
                cfg.CreateMap<ExerciseSearchResult, ExerciseSearchResult>();
                cfg.CreateMap<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>();

                cfg.CreateMap<FileAttachmentModel, FileAttachment>();
                cfg.CreateMap<FileAttachment, FileAttachmentModel>();
                cfg.CreateMap<FileAttachment, FileAttachment>();
                cfg.CreateMap<FileAttachment, FileAttachmentSearchResult>();
                cfg.CreateMap<FileAttachmentSearchResult, FileAttachment>();
                cfg.CreateMap<FileAttachmentSearchResult, FileAttachmentSearchResult>();
                cfg.CreateMap<FileAttachmentSearchCriteriaViewModel, FileAttachmentSearchCriteria>();

                cfg.CreateMap<GoalModel, Goal>();
                cfg.CreateMap<Goal, GoalModel>();
                cfg.CreateMap<Goal, Goal>();
                cfg.CreateMap<Goal, GoalSearchResult>();
                cfg.CreateMap<GoalSearchResult, Goal>();
                cfg.CreateMap<GoalSearchResult, GoalSearchResult>();
                cfg.CreateMap<GoalSearchCriteriaViewModel, GoalSearchCriteria>();
            });
        }
    }
}
