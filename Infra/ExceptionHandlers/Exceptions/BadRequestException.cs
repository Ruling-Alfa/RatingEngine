using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infra.ExceptionHandlers.Exceptions
{
    public class BadRequestException : BaseException
    {
        public string ExceptionDetails { get; set; }
        public BadRequestException(string Message) : base(Message)
            => ExceptionDetails = Message;

        public BadRequestException() : base() =>
            ExceptionDetails = "Bad Request";
        public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, ILogger logger, CancellationToken cancellationToken)
        {
            if (exception is not BadRequestException badRequestException)
            {
                return false;
            }

            return await ExceptionResponseBuilder.BuildExceptionResponse(httpContext, badRequestException, HttpStatusCode.BadRequest, logger, IsProdEnv, cancellationToken);
        }
    }
}