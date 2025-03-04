using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;
using Infra.ExceptionHandlers.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Infra.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            return exception switch
            {
                BaseException baseException => await baseException.TryHandleAsync(httpContext, exception, _logger, cancellationToken),

                _ => await ExceptionResponseBuilder.BuildExceptionResponse(httpContext, exception,
                                HttpStatusCode.InternalServerError, _logger, BaseException.IsProdEnv, cancellationToken)
            };
        }
    }
}
