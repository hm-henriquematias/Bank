using System;

namespace Bank.Business.Domain.Validators
{
    public class StringValidator
    {
        public static bool ValidateString(string str)
        {
            return !String.IsNullOrEmpty(str);
        }
    }
}
