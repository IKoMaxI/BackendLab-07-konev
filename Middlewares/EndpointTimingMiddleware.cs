using System.Diagnostics;

namespace Lab7.Middlewares
{
    public class EndpointTimingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var sw = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                sw.Stop();
                context.Response.Headers.Append(
                    "X-Endpoint-Elapsed-Ms",
                    ((long)sw.Elapsed.TotalMilliseconds).ToString());

                return Task.CompletedTask;
            });

            await next(context);
        }
    }
}
