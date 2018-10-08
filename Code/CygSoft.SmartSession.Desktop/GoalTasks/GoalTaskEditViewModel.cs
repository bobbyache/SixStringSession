using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.GoalTasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CygSoft.SmartSession.Desktop.GoalTasks
{
    public class GoalTaskEditViewModel : ViewModelBase
    {
        private IGoalTaskService goalTaskService;
        private IDialogViewService dialogService;

        private GoalTaskModel goalTaskModel;
        private GoalTaskSearchResultModel goalTaskSearchResult;

        public GoalTaskModel GoalTask
        {
            get { return goalTaskModel; }
            private set
            {
                Set(() => GoalTask, ref goalTaskModel, value);
            }
        }

        public GoalTaskEditViewModel(IGoalTaskService goalTaskService, IDialogViewService dialogService)
        {
            this.goalTaskService = goalTaskService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !goalTaskModel.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public void StartEdit(GoalTaskSearchResultModel goalTaskSearchResult)
        {
            this.goalTaskSearchResult = goalTaskSearchResult;

            if (this.GoalTask != null) this.GoalTask.ErrorsChanged -= GoalTask_ErrorsChanged;
            this.GoalTask = new GoalTaskModel(this.goalTaskService.Get(goalTaskSearchResult.Id));
            this.GoalTask.ErrorsChanged += GoalTask_ErrorsChanged;
        }

        private void GoalTask_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            goalTaskModel.Commit();
            goalTaskService.Update(goalTaskModel.GoalTask);

            if (goalTaskSearchResult != null)
                Mapper.Map(goalTaskModel, goalTaskSearchResult);

            Messenger.Default.Send(new EndEditingGoalTaskMessage(goalTaskModel));
        }

        private void Cancel()
        {
            goalTaskModel.Revert();

            if (goalTaskSearchResult != null)
                Mapper.Map(goalTaskModel, goalTaskSearchResult);

            Messenger.Default.Send(new EndEditingGoalTaskMessage(goalTaskModel));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
