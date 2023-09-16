using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ExceptionLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var exceptionLog = new ExceptionLog();

                    exceptionLog.Timestamp = DateTime.UtcNow;
                      exceptionLog.Message = ex.Message;
                    exceptionLog.StackTrace = ex.StackTrace;
                    

                    // Capture additional exception location information
                    var stackTrace = new StackTrace(ex, true);
                    if (stackTrace.FrameCount > 0)
                    {
                        var frame = stackTrace.GetFrame(0);
                        exceptionLog.FileName = frame.GetFileName() == null? "NoName": frame.GetFileName();
                        exceptionLog.LineNumber = frame.GetFileLineNumber() == 0 ? 0 : frame.GetFileLineNumber();
                        exceptionLog.ClassName = frame.GetMethod().DeclaringType.FullName == null ? "NoClassName" : frame.GetMethod().DeclaringType.FullName;
                    }
                    dbContext.ExceptionLogs.Add(exceptionLog);
                    await dbContext.SaveChangesAsync();
                }

                // Rethrow the exception to propagate it to the global exception handler if needed.
                throw;
            }

        }
    }
}

