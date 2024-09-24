using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Quartz.Shared.Middlewares
{
    
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add(CorrelationIdHeader, correlationId);
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationIdHeader, correlationId);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }

}
