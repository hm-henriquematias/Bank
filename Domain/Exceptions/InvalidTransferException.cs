using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Domain.Exceptions
{
    public class InvalidTransferException : Exception
    {
        public InvalidTransferException()
        {
        }

        public InvalidTransferException(string message) : base(message)
        {
        }
    }
}
