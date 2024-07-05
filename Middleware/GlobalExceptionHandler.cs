using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TodoAPI.Models;

namespace TodoAPI.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            _logger.LogError($"An error occurred while processing your request: {exception.Message}");

            var errorResponse = new ErrorResponse
            {
                ErrorMsg = exception.Message
            };

            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.Stat

            }
        }
    }
}