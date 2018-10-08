using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CygSoft.SmartSession.Desktop.Supports.Validators
{
    public class ValidatableObservableObject : ObservableObject, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool TrackChanges { get; set; }

        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                Set(() => IsDirty, ref isDirty, value);
            }
        }

        private ValidatableObject validator;

        public bool HasErrors => validator.HasErrors;

        public IEnumerable GetErrors(string propertyName)
        {
            return validator.GetErrors(propertyName);
        }

        public ValidatableObservableObject()
        {
            validator = new ValidatableObject(this);
            validator.ErrorsChanged += Validator_ErrorsChanged;
        }

        private void Validator_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            if (this.ErrorsChanged != null)
                ErrorsChanged(this, e);
        }

        public void Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue, bool validate, bool isDirty = false)
        {
            if (Set(propertyExpression, ref field, newValue))
            {
                if (validate)
                    validator.Validate(propertyExpression, newValue);
                
                if (TrackChanges)
                    IsDirty = isDirty;
            }
        }

        public virtual void Revert() { IsDirty = false; }

        public virtual void Commit() { IsDirty = false; }
    }
}
