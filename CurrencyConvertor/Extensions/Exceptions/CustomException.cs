using System;

namespace CurrencyConvertor.Extensions.Exceptions
{
    /// <summary>
    /// Custom exception to aid in Exception Middleware
    /// </summary>
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
