using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PantryPilot.Api.ExceptionHandling
{
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception of type {ExceptionType} occurred.", exception.GetType().Name);

            var problemDetails = exception switch
            {
                ArgumentException ex => new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = ex.Message
                },

                InvalidOperationException ex => new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Invalid Operation",
                    Detail = ex.Message
                },

                TaskCanceledException => new ProblemDetails
                {
                    Status = StatusCodes.Status408RequestTimeout,
                    Title = "Request Cancelled",
                    Detail = "The request was cancelled."
                },

                _ => new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred."
                }
            };

            httpContext.Response.StatusCode = problemDetails.Status!.Value;

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails,
                cancellationToken);

            return true;
        }
    }
}
