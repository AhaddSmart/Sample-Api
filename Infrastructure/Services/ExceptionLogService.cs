using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExceptionLogService
    {
        private readonly ApplicationDbContext _dbContext;

        public ExceptionLogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogException(Exception ex)
        {
            var exceptionLog = new ExceptionLog
            {
                Timestamp = DateTime.UtcNow,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };

            _dbContext.ExceptionLogs.Add(exceptionLog);
            _dbContext.SaveChanges();
        }
    }
}
