using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly ApplicationDbContext _dbContext;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, ApplicationDbContext dbContext)
        {
            _next = next;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log request information before processing
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            // Capture the response status code
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            // Log response information after processing
            var responseContent = await FormatResponseAsync(context.Response);

            // Log to the database
            _dbContext.LogEntries.Add(new LogEntry
            {
                logLevel = "Information",
                message = $"Request: {context.Request.Method} {context.Request.Path}, Response: {context.Response.StatusCode}, {responseContent}"
            });

            await _dbContext.SaveChangesAsync();

            // Restore the original response body
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatResponseAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return responseBody;
        }
    }
}
