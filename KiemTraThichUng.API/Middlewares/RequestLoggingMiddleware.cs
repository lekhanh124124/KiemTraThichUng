// File: KiemTraThichUng.API/Middlewares/RequestLoggingMiddleware.cs
using Serilog;
using System.Diagnostics;

namespace KiemTraThichUng.API.Middlewares
{
    public sealed class RequestLoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();

            Log.Information(
                "HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
