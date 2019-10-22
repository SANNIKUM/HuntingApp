using System;

namespace AFFHA.API.Infrastructure.Shared.Exceptions
{
    public class IncorrectOldPassword : Exception
    {
        public IncorrectOldPassword() : base("Old password you entered is incorrect.")
        {

        }
    }
}
