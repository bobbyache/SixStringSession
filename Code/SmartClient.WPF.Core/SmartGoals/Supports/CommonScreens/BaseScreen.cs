using Caliburn.Micro;
using SmartGoals.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartGoals.Supports.CommonScreens
{
    public class BaseScreen : Screen
    {
        private readonly IEventAggregator eventAggregator;
        protected readonly IDialogService dialogService;
        protected readonly ISettingsService settingsService;

        public BaseScreen(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService)
        {
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;
            this.settingsService = settingsService;

            this.eventAggregator.SubscribeOnUIThread(this);
        }

        public void Notify(object message)
        {
            eventAggregator.PublishOnUIThreadAsync(message);
        }
    }
}
