using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Infra.ExceptionHandlers
{
    public static class ExceptionResponseBuilder
    {
        public static async ValueTask<bool> BuildExceptionResponse(HttpContext httpContext, Exception exception,
            HttpStatusCode httpStatusCode, ILogger logger, bool maskDetailsInResponse = false, CancellationToken cancellationToken = default)
        {
            logger.LogError(exception, "{HttpStatusCode}: {RequestUri} {Message} ",
                        httpStatusCode,
                        httpContext.Request.GetDisplayUrl(),
                        exception.Message);

            var problemDetails = new ProblemDetails
            {
                Title = httpStatusCode.ToString(),
                Status = (int)httpStatusCode,
                Detail = maskDetailsInResponse ? httpStatusCode.ToString() : exception.Message,
                Instance = httpContext.Request.GetDisplayUrl()
            };

            httpContext.Response.StatusCode = (int)httpStatusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
