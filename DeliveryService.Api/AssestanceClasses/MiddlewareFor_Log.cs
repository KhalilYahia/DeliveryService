using Serilog;

namespace DeliveryService.WebApi.AssestanceClasses
{
    public class MiddlewareFor_Log
    {

        private readonly RequestDelegate _next;

        public MiddlewareFor_Log(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            // Add a separator or horizontal line before logging each request
            Log.Information("-----------------------------------------------------------------------------------------");
            Log.Information("-----------------------------------------------------------------------------------------");

            Log.Information("New request: {Method} {Path}", context.Request.Method, context.Request.Path);

            // Call the next middleware in the pipeline
            await _next(context);

          
        }
    }
}
