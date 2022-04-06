using System;

namespace CurrencyConvertor.Extensions.Exceptions
{
    /// <summary>
    /// Invalid login attempt Custom Exception
    /// </summary>
    public class InvalidLoginException : CustomException
    {
        public InvalidLoginException() : base("Invalid login attempt.")
        {
        }
    }
}