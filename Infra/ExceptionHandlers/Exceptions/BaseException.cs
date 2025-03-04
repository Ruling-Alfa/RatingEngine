using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.ExceptionHandlers.Exceptions
{
    public abstract class BaseException : Exception
    {
        public static bool IsProdEnv
        {
            get
            {
                var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                ArgumentNullException.ThrowIfNull(currentEnv, nameof(currentEnv));

                return string.Equals(currentEnv!.Trim(),
                    Environments.Production,
                    StringComparison.OrdinalIgnoreCase);
            }
        }
        protected BaseException() : base() { }
        protected BaseException(string Message) : base(Message) { }
        public abstract ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, ILogger logger, CancellationToken cancellationToken);
    }
}
