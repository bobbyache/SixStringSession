using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Goals;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CygSoft.SmartSession.Desktop.Goals
{
    public class GoalEditViewModel : ViewModelBase
    {
        private IGoalService goalService;
        private IDialogViewService dialogService;

        private GoalModel goalModel;
        private GoalSearchResultModel goalSearchResult;

        public GoalModel Goal
        {
            get { return goalModel; }
            private set
            {
                Set(() => Goal, ref goalModel, value);
            }
        }

        public GoalEditViewModel(IGoalService goalService, IDialogViewService dialogService)
        {
            this.goalService = goalService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            SaveCommand = new RelayCommand(() => Save(), () => !goalModel.HasErrors);
            CancelCommand = new RelayCommand(() => Cancel(), () => true);
        }

        public void StartEdit(GoalSearchResultModel goalSearchResult)
        {
            this.goalSearchResult = goalSearchResult;

            if (this.Goal != null) this.Goal.ErrorsChanged -= Goal_ErrorsChanged;
            this.Goal = new GoalModel(this.goalService.Get(goalSearchResult.Id));
            this.Goal.ErrorsChanged += Goal_ErrorsChanged;
        }

        private void Goal_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Save()
        {
            goalModel.Commit();
            goalService.Update(goalModel.Goal);

            if (goalSearchResult != null)
                Mapper.Map(goalModel, goalSearchResult);

            Messenger.Default.Send(new EndEditingGoalMessage(goalModel));
        }

        private void Cancel()
        {
            goalModel.Revert();

            if (goalSearchResult != null)
                Mapper.Map(goalModel, goalSearchResult);

            Messenger.Default.Send(new EndEditingGoalMessage(goalModel));
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
