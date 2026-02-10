using System.Net;

namespace Project.UI.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluþtu. TraceId: {TraceId}", context.TraceIdentifier);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { success = false, message = "Sunucuda bir hata oluþtu." });
                }
                else
                {
                    context.Response.Redirect("/Home/Error");
                }
            }
        }
    }
}
