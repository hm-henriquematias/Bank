using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Domain.Exceptions
{
    public class InvalidCustomerAccountException : Exception
    {
        public InvalidCustomerAccountException()
        {
        }

        public InvalidCustomerAccountException(string message) : base(message)
        {
        }
    }
}
