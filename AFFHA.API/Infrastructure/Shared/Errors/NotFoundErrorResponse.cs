using System.Net;

namespace AFFHA.API.Infrastructure.Shared.Errors
{
    public class NotFoundErrorResponse : ErrorResponse
    {
        public NotFoundErrorResponse() : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundErrorResponse(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}