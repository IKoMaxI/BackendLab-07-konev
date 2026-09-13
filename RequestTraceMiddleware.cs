namespace Lab7.Middlewares
{
    public class RequestTraceMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var traceId = Guid.NewGuid().ToString();

            context.Response.Headers.Append("X-Trace-Id", traceId);
            context.Items["TraceId"] = traceId;

            await next(context);
        }
    }
}
