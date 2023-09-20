using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public LoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DateTime requestTime = DateTime.UtcNow;

            // Log request information before processing
            var requestLogEntry = new LogEntry
            {
                logLevel = "Information",
                message = $"Request: {context.Request.Method} {context.Request.Path}",
                requestTime = DateTime.UtcNow,
            };

            // Capture the response status code
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await dbContext.LogEntries.AddAsync(requestLogEntry);
            await dbContext.SaveChangesAsync();

            int id = requestLogEntry.Id;
            context.Items["id"] = requestLogEntry.Id;

            await _next(context);

            // Log response information after processing
            var entity = await dbContext.LogEntries.FindAsync(id);
            entity.message = $"Request: {context.Request.Method} {context.Request.Path}, Response: {context.Response.StatusCode}";
            entity.responseTime = DateTime.UtcNow;

            //Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5,  None = 6.


            await dbContext.SaveChangesAsync();

            // Restore the original response body
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}

