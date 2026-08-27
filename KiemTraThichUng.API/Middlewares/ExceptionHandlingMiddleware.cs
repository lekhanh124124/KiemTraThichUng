// File: KiemTraThichUng.API/Middlewares/ExceptionHandlingMiddleware.cs
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Domain.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace KiemTraThichUng.API.Middlewares
{
    public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = context.TraceIdentifier;

                Log.Error(ex,
                    "Unhandled exception. CorrelationId: {CorrelationId}, Method: {Method}, Path: {Path}",
                    traceId,
                    context.Request.Method,
                    context.Request.Path);

                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            string traceId)
        {
            context.Response.ContentType = "application/json";

            (HttpStatusCode statusCode, List<ApiMessage> errors) = exception switch
            {
                ValidationException v =>
                    (HttpStatusCode.BadRequest,
                     v.Errors.Select(e => new ApiMessage("VALIDATION_ERROR", e)).ToList()),

                NotFoundException nf =>
                    (HttpStatusCode.NotFound,
                     nf.Errors.Select(e => new ApiMessage("NOT_FOUND", e)).ToList()),

                ConflictException cf =>
                    (HttpStatusCode.Conflict,
                     cf.Errors.Select(e => new ApiMessage("CONFLICT", e)).ToList()),

                DomainValidationException dv =>
                    (HttpStatusCode.BadRequest,
                     new List<ApiMessage> { new("DOMAIN_VALIDATION", dv.Message) }),

                UnauthorizedAccessException =>
                    (HttpStatusCode.Unauthorized,
                     new List<ApiMessage> { new("UNAUTHORIZED", "Unauthorized") }),

                _ =>
                    (HttpStatusCode.InternalServerError,
                     new List<ApiMessage>
                     {
                     new("SERVER_ERROR",
                         $"An unexpected error occurred. CorrelationId: {traceId}")
                     })
            };

            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse<object>.Failure(errors);

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
