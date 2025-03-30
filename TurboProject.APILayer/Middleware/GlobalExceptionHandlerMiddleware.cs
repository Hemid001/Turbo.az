using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;
using TurboProject.BusinessLayer.Model.ApiResponse;

namespace TurboProject.APILayer.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await next(httpContext);
            }


            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                var response = new ApiResponse<string>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "An unexpected error occurred. Please try again later." }
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
