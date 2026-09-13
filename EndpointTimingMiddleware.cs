using System.Diagnostics;
using System.Globalization;

namespace Lab7.Middlewares;

public class EndpointTimingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var originalBody = context.Response.Body;
        await using var buffer = new MemoryStream();
        context.Response.Body = buffer;
        var stopwatch = Stopwatch.StartNew();
        try
        {
            // Буфер откладывает отправку ответа до установки заголовка.
            await next(context);
            stopwatch.Stop();
            context.Response.Headers["X-Endpoint-Elapsed-Ms"] =
                stopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);
            context.Response.Body = originalBody;
            buffer.Position = 0;
            await buffer.CopyToAsync(originalBody, context.RequestAborted);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }
}
