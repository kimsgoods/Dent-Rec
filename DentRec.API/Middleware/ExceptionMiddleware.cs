using System;
using System.Net;
using System.Text.Json;

namespace DentRec.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest, // 400
                KeyNotFoundException => (int)HttpStatusCode.NotFound, // 404
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
                _ => (int)HttpStatusCode.InternalServerError // 500
            };

            var errorResponse = new { message = ex.Message, statusCode = response.StatusCode };
            return response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
