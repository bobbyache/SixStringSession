using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.Validators
{
    public static class ValidatorFuncs
    {
        public static bool TextIsMetronomeSpeed(string text)
        {
            return TextIsInteger(text);
        }

        public static bool TextIsInteger(string text)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            return regex.IsMatch(text);
        }
    }
}
