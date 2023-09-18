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

                    CaptureExceptionLocation(ex, exceptionLog);

                    // Capture additional exception location information
                    //var stackTrace = new StackTrace(ex, true);
                    //if (stackTrace.FrameCount > 0)
                    //{
                    //    var frame = stackTrace.GetFrame(0);
                    //    exceptionLog.FileName = frame.GetFileName() == null ? "NoName" : frame.GetFileName();
                    //    exceptionLog.LineNumber = frame.GetFileLineNumber() == 0 ? 0 : frame.GetFileLineNumber();
                    //    exceptionLog.ClassName = frame.GetMethod().DeclaringType.FullName == null ? "NoClassName" : frame.GetMethod().DeclaringType.FullName;
                    //}
                    dbContext.ExceptionLogs.Add(exceptionLog);
                    await dbContext.SaveChangesAsync();
                }

                // Rethrow the exception to propagate it to the global exception handler if needed.
                throw;
            }

        }
        private static void CaptureExceptionLocation(Exception ex, ExceptionLog exceptionLog)
        {
            var stackTrace = new StackTrace(ex, true);
            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method != null)
                {
                    var declaringType = method.DeclaringType;
                    if (declaringType != null)
                    {
                        exceptionLog.ClassName = declaringType.FullName == null ? "NoName" : declaringType.FullName;
                        exceptionLog.FileName = frame.GetFileName() == null ? "NoClassName" : frame.GetFileName();
                        exceptionLog.LineNumber = frame.GetFileLineNumber() == 0 ? 0 : frame.GetFileLineNumber();
                        break;
                    }
                }
            }
        }

        //private static void CaptureExceptionLocation(Exception ex, ExceptionLog exceptionLog)
        //{
        //    if (ex == null || exceptionLog == null)
        //    {
        //        return; // Check for null references
        //    }

        //    var stackTrace = new StackTrace(ex, true);
        //    foreach (var frame in stackTrace.GetFrames())
        //    {
        //        var method = frame.GetMethod();
        //        if (method != null)
        //        {
        //            var declaringType = method.DeclaringType;
        //            if (declaringType != null)
        //            {
        //                // Initialize the ExceptionLog object if necessary
        //                if (exceptionLog.ClassName == null)
        //                {
        //                    exceptionLog.ClassName = declaringType.FullName;
        //                }

        //                if (exceptionLog.FileName == null)
        //                {
        //                    exceptionLog.FileName = frame.GetFileName();
        //                }

        //                if (exceptionLog.LineNumber == 0)
        //                {
        //                    exceptionLog.LineNumber = frame.GetFileLineNumber();
        //                }

        //                // Break after the first frame with valid information
        //                break;
        //            }
        //        }
        //    }
        //}

    }

}

