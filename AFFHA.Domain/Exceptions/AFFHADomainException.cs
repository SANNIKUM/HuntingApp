namespace AFFHA.Domain.Exceptions
{
    using System;
   public class AFFHADomainException : Exception
    {
        public AFFHADomainException()
        { }

        public AFFHADomainException(string message)
            : base(message)
        { }

        public AFFHADomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
