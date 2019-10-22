using System;

namespace AFFHA.API.Infrastructure.Shared.Exceptions
{
    public class InvalidLoginCredentialsException : Exception
    {
        public InvalidLoginCredentialsException() : base("The email or password you entered is incorrect.")
        {
        }
    }
}