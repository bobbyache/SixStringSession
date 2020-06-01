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
        protected readonly IDialogService Dialogs;
        protected readonly ISettingsService Settings;

        public BaseScreen(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService)
        {
            this.eventAggregator = eventAggregator;
            this.Dialogs = dialogService;
            this.Settings = settingsService;

            this.eventAggregator.SubscribeOnUIThread(this);
        }

        public void Notify(object message)
        {
            eventAggregator.PublishOnUIThreadAsync(message);
        }
    }
}
