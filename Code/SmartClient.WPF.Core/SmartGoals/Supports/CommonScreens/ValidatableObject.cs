using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SmartGoals.Supports.CommonScreens
{
    public class ValidatableObject : INotifyDataErrorInfo
    {
        private object contextObject;

        public ValidatableObject(object contextObject)
        {
            this.contextObject = contextObject;
        }

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public bool HasErrors => errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                return errors[propertyName];
            else
                return null;
        }

        public void ValidateAll()
        {
            var properties = TypeDescriptor.GetProperties(this.contextObject);
            foreach (var prop in properties)
            {
                PropertyDescriptor p = (PropertyDescriptor)prop;
                if (p.Attributes.OfType<ValidationAttribute>().Any())
                {
                    Validate(p.Name, p.GetValue(this.contextObject));
                }
            }
        }

        public void Validate<T>(Expression<Func<T>> propertyExpression, T newValue)
        {
            string propertyName = GetPropertyName<T>(propertyExpression);
            Validate(propertyName.ToString(), newValue);
        }

        public void Validate<T>(string propertyName, T newValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(contextObject);
            context.MemberName = propertyName;
            Validator.TryValidateProperty(newValue, context, results);

            if (results.Any())
            {
                errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                errors.Remove(propertyName);
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }
    }
}
