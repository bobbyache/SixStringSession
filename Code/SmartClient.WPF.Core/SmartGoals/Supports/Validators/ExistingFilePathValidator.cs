using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SmartGoals.Supports.Validators
{
    public class ExistingFilePathValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return File.Exists((string)value);
        }
    }
}
