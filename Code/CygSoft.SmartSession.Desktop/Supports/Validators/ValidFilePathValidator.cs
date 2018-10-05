using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.Validators
{
    public class ValidFilePathValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return File.Exists((string)value);
        }
    }
}
