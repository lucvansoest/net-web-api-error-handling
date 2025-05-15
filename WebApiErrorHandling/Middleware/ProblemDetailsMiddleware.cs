using Microsoft.AspNetCore.Mvc;
using WebApiErrorHandling.Models;

namespace WebApiErrorHandling.Middleware
{
    public class ProblemDetailsMiddleware(RequestDelegate next, ILogger<ProblemDetailsMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred");

                var problem = new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var errorResponse = new ApiResponse<object>
                {
                    Success = false,
                    Data = null,
                    Error = problem
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
