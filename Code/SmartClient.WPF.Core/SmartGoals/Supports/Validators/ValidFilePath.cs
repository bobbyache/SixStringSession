using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SmartGoals.Supports.Validators
{
    public class ValidFilePath : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string path = (string)value;
            return !string.IsNullOrEmpty(path)
                && Path.IsPathRooted(path)
                && path.IndexOfAny(Path.GetInvalidPathChars()) == -1
                && Path.HasExtension(path)
                ;
        }
    }
}
