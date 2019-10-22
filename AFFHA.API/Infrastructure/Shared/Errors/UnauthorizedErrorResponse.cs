namespace AFFHA.API.Infrastructure.Shared.Errors
{
    using System.Net;

    public class UnauthorizedErrorResponse : ErrorResponse
    {
        public UnauthorizedErrorResponse(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}