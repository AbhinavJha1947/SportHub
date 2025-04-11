﻿using System.Net;
using System.Text.Json;

namespace SportHub.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                ApplicationException => HttpStatusCode.BadRequest,
                // Add other exception types as needed
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var errorDetails = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                DetailedMessage = exception.InnerException?.Message
            };

            var json = JsonSerializer.Serialize(errorDetails);
            await context.Response.WriteAsync(json);
        }
    }
}
