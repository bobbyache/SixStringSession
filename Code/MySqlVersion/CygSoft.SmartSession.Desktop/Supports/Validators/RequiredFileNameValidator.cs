﻿using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace CygSoft.SmartSession.Desktop.Supports.Validators
{
    public class RequiredFileNameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool valid = !((string)value).Any(f => Path.GetInvalidFileNameChars().Contains(f));

            if (!valid)
                return new ValidationResult(false, "Valid file title must be supplied.");
            else
                return new ValidationResult(true, null);
        }
    }
}
