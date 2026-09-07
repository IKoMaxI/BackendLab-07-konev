namespace Lab7.Middlewares
{
    public class BlockPathMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments("/blocked"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
