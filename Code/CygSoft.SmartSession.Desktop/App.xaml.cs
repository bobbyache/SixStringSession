using AutoMapper;
using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Exercises;
using CygSoft.SmartSession.Desktop.Goals;
using CygSoft.SmartSession.Desktop.GoalTasks;
using CygSoft.SmartSession.Domain.Attachments;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Domain.Goals;
using CygSoft.SmartSession.Domain.GoalTasks;
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
                cfg.CreateMap<Exercise, ExerciseSearchResultModel>();
                cfg.CreateMap<ExerciseSearchResultModel, Exercise>();
                cfg.CreateMap<ExerciseSearchResultModel, ExerciseSearchResultModel>();
                cfg.CreateMap<ExerciseSearchCriteriaViewModel, ExerciseSearchCriteria>();

                cfg.CreateMap<FileAttachmentModel, FileAttachment>();
                cfg.CreateMap<FileAttachment, FileAttachmentModel>();
                cfg.CreateMap<FileAttachment, FileAttachment>();
                cfg.CreateMap<FileAttachment, FileAttachmentSearchResultModel>();
                cfg.CreateMap<FileAttachmentSearchResultModel, FileAttachment>();
                cfg.CreateMap<FileAttachmentSearchResultModel, FileAttachmentSearchResultModel>();
                cfg.CreateMap<FileAttachmentSearchCriteriaViewModel, FileAttachmentSearchCriteria>();

                cfg.CreateMap<GoalModel, Goal>();
                cfg.CreateMap<Goal, GoalModel>();
                cfg.CreateMap<Goal, Goal>();
                cfg.CreateMap<Goal, GoalSearchResultModel>();
                cfg.CreateMap<GoalSearchResultModel, Goal>();
                cfg.CreateMap<GoalSearchResultModel, GoalSearchResultModel>();
                cfg.CreateMap<GoalSearchCriteriaViewModel, GoalSearchCriteria>();

                cfg.CreateMap<GoalTaskModel, GoalTask>();
                cfg.CreateMap<GoalTask, GoalTaskModel>();
                cfg.CreateMap<GoalTask, GoalTask>();
                cfg.CreateMap<GoalTask, GoalTaskSearchResultModel>();
                cfg.CreateMap<GoalTaskSearchResultModel, GoalTask>();
                cfg.CreateMap<GoalTaskSearchResultModel, GoalTaskSearchResultModel>();
                cfg.CreateMap<GoalTaskSearchCriteriaViewModel, GoalTaskSearchCriteria>();
            });
        }
    }
}
