namespace ProjectAssignment.Middleware
{
    public class Errorhandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Errorhandling> _logger;

        public Errorhandling(RequestDelegate next, ILogger<Errorhandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred."); 
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new { message = "An unexpected error occurred." };
            return context.Response.WriteAsJsonAsync(response); 
        }
    }
}
