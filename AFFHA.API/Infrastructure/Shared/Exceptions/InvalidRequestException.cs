namespace AFFHA.API.Infrastructure.Shared.Exceptions
{
    using System;

    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() : base("Invalid Request")
        {
        }

        public InvalidRequestException(string message) : base(message)
        {
        }

        public InvalidRequestException(Type type, int id)
            : base($"Could not find any {type.Name} with id {id}")
        {
        }
    }
}