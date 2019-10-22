using System;

namespace AFFHA.API.Infrastructure.Shared.Exceptions
{
    public class FileIsTooLargeException : Exception
    {
        public FileIsTooLargeException(string message) : base(message ="File size is too lare")
        {

        }
    }
}
