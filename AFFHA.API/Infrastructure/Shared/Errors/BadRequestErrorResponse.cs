using System.Net;

namespace AFFHA.API.Infrastructure.Shared.Errors
{
    public class BadRequestErrorResponse : ErrorResponse
    {
        public BadRequestErrorResponse() : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestErrorResponse(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}