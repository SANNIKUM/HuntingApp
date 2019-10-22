using System;

namespace AFFHA.API.Infrastructure.Shared.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(Type type, int id)
            : base($"Could not find any {type.Name} with id {id}")
        {
        }
    }
}