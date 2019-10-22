namespace AFFHA.API.Infrastructure.Shared.Errors
{
    using System.Net;

    public class Error
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusText { get; set; }
        public string Message { get; set; }
        public string[] ValidationErrors { get; set; }
    }

    public class ErrorResponse
    {
        public Error Error { get; set; }

        public ErrorResponse(string message)
        {
            this.Error = new Error
            {
                StatusCode = HttpStatusCode.InternalServerError,
                StatusText = GetStatusText(HttpStatusCode.InternalServerError),
                Message = message ?? "Unknown error occurred.",
            };
        }

        public ErrorResponse(HttpStatusCode status)
        {
            this.Error = new Error
            {
                StatusCode = status,
                StatusText = GetStatusText(status),
                Message = GetDefaultMessage(status),
            };
        }

        public ErrorResponse(HttpStatusCode status, string message)
        {
            this.Error = new Error
            {
                StatusCode = status,
                StatusText = GetStatusText(status),
                Message = message,
            };
        }

        private string GetDefaultMessage(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.NotFound:
                    return "Not Found";
                default:
                    return "Something went wrong.";
            }
        }

        private string GetStatusText(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.BadRequest:
                    return "Bad Request";
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized";
                case HttpStatusCode.Forbidden:
                    return "Forbidden";
                case HttpStatusCode.NotFound:
                    return "Not Found";
                case HttpStatusCode.InternalServerError:
                    return "Internal Server Error";
                default:
                    return null;
            }
        }
    }
}