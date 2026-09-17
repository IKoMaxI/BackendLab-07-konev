using Lab7.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<BlockPathMiddleware>();
builder.Services.AddTransient<RequestTraceMiddleware>();
builder.Services.AddTransient<EndpointTimingMiddleware>();

var app = builder.Build();

app.UseMiddleware<BlockPathMiddleware>();
app.UseMiddleware<RequestTraceMiddleware>();
app.UseMiddleware<EndpointTimingMiddleware>();

app.MapGet("/", () => Results.Content("""
<!doctype html>
<html lang="ru">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Лабораторная работа № 7</title>
</head>
<body>
    <h1>Приложение запущено</h1>
    <ul>
        <li><a href="/ping">GET /ping</a> — проверка ответа pong</li>
        <li><a href="/trace">GET /trace</a> — просмотр TraceId</li>
        <li><a href="/error">GET /error</a> — тест исключения</li>
        <li><a href="/blocked">GET /blocked</a> — проверка 403 Forbidden</li>
    </ul>
</body>
</html>
""", "text/html; charset=utf-8"));

app.MapGet("/ping", () =>
{
    return "pong";
});

app.MapGet("/trace", (HttpContext context) =>
{
    return Results.Ok(context.Items["TraceId"]);
});

app.MapGet("/error", IResult () =>
{
    throw new Exception("Error for handling");
});

app.Run();
