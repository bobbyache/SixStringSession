using Caliburn.Micro;
using SmartGoals.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace SmartGoals.Supports.CommonScreens
{
    public class ValidatableScreen : BaseScreen, INotifyDataErrorInfo
    {
        public ValidatableScreen(IEventAggregator eventAggregator, IDialogService dialogService, ISettingsService settingsService) 
            : base(eventAggregator, dialogService, settingsService)
        {
            validator = new ValidatableObject(this);
            validator.ErrorsChanged += Validator_ErrorsChanged;
        }

        public bool CanSubmit
        {
            get { return !validator.HasErrors; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected ValidatableObject validator;

        public bool HasErrors => validator.HasErrors;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return validator.GetErrors(propertyName);
        }

        private void Validator_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            if (this.ErrorsChanged != null)
                ErrorsChanged(this, e);
        }


        public void SetAndValidate<T>(Expression<Func<T>> propertyExpression, T newValue)
        {
            NotifyOfPropertyChange(propertyExpression);
            Validate(propertyExpression, newValue);
            NotifyOfPropertyChange(() => CanSubmit);
        }

        public void Validate<T>(Expression<Func<T>> propertyExpression, T newValue)
        {
            validator.Validate(propertyExpression, newValue);
        }

        public virtual void ValidateAll() { validator.ValidateAll(); }
    }
}
