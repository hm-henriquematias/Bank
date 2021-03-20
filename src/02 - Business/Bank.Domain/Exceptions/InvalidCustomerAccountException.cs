using System;

namespace Bank.Business.Domain.Exceptions
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
