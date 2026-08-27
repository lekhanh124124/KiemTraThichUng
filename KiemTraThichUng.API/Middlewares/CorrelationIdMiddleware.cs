// File: KiemTraThichUng.API/Middlewares/CorrelationIdMiddleware.cs
using Serilog.Context;

namespace KiemTraThichUng.API.Middlewares
{
    public sealed class CorrelationIdMiddleware(RequestDelegate next)
    {
        private const string HeaderName = "X-Correlation-Id";

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[HeaderName].FirstOrDefault()
                                ?? Guid.NewGuid().ToString();

            context.TraceIdentifier = correlationId;
            context.Response.Headers[HeaderName] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next(context);
            }
        }
    }
}
