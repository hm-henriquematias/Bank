using System;

namespace Bank.Business.Domain.Exceptions
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
