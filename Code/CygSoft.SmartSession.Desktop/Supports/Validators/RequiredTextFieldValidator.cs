﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CygSoft.SmartSession.Desktop.Supports.Validators
{
    public class RequiredTextFieldValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (((string)value).Length < 4)
                return new ValidationResult(false, "Field value must be supplied.");
            //if (string.IsNullOrEmpty((string)value))
            //    return new ValidationResult(false, "Field value must be supplied.");
            else
                return new ValidationResult(true, null);
        }
    }
}
