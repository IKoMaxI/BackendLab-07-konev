using Lab7.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<BlockPathMiddleware>();
builder.Services.AddTransient<RequestTraceMiddleware>();
builder.Services.AddTransient<EndpointTimingMiddleware>();

var app = builder.Build();

app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();

app.MapGet("/ping", () =>
{
    return "pong";
});

app.MapGet("/trace", (HttpContext context) =>
{
    return Results.Ok(context.Items["TraceId"]);
});

app.MapGet("/error", () =>
{
    throw new Exception("Error for handling");
});

app.Run();
