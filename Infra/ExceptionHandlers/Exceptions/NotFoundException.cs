using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infra.ExceptionHandlers.Exceptions
{
    public class NotFoundException : BaseException
    {
        public string ExceptionDetails { get; set; }
        public NotFoundException(string Message) : base(Message)
            => ExceptionDetails = Message;

        public NotFoundException() : base() =>
            ExceptionDetails = "Not Found";
        public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, ILogger logger, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException notFoundException)
            {
                return false;
            }

            return await ExceptionResponseBuilder.BuildExceptionResponse(httpContext, notFoundException, HttpStatusCode.NotFound, logger, IsProdEnv, cancellationToken);
        }
    }
}