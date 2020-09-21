using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.Domain.Validators
{
    public class StringValidator
    {
        public static bool ValidateString(string str)
        {
            return !String.IsNullOrEmpty(str);
        }
    }
}
