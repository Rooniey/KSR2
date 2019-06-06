using System;

namespace FuzzyLogic.Exceptions
{
    class UnknownFuzzySetsOperationException : Exception
    {
        public UnknownFuzzySetsOperationException()
        {
        }

        public UnknownFuzzySetsOperationException(string message) : base(message)
        {
        }

        public UnknownFuzzySetsOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
