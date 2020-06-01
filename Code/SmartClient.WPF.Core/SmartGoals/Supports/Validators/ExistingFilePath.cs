using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace SmartGoals.Supports.Validators
{
    public class ExistingFilePath : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return File.Exists((string)value);
        }
    }
}
