using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure
//{
//    public class GlobalExceptionHandlerMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
//        private readonly ExceptionLogService _exceptionLogService;

//        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, ExceptionLogService exceptionLogService)
//        {
//            _next = next;
//            _logger = logger;
//            _exceptionLogService = exceptionLogService;
//        }
//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                // Determine if the exception was handled or unhandled
//                var exceptionType = context.Items["ExceptionType"] as string;

//                if (exceptionType == "Handled")
//                {
//                    _logger.LogInformation(ex, "A handled exception occurred");
//                }
//                else if (exceptionType == "Unhandled")
//                {
//                    _logger.LogError(ex, "An unhandled exception occurred");
//                    _exceptionLogService.LogException(ex); // Log to the database
//                }
//            }
//        }
//    }
//}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly ExceptionLogService _exceptionLogService;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, ExceptionLogService exceptionLogService)
        {
            _next = next;
            _logger = logger;
            _exceptionLogService = exceptionLogService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Determine if the exception was handled or unhandled
                var exceptionType = context.Items["ExceptionType"] as string;

                if (exceptionType == "Handled")
                {
                    _logger.LogInformation(ex, "A handled exception occurred");
                }
                else if (exceptionType == "Unhandled")
                {
                    _logger.LogError(ex, "An unhandled exception occurred");
                    // You can log to the database or perform other actions here
                    _exceptionLogService.LogException(ex); // Log to the database
                }
            }
        }
    }
}