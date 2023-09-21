using Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [CreateService(false)]
    public class ExceptionLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string? FileName { get; set; } 
        public int? LineNumber { get; set; }  
        public string? ClassName { get; set; }
        public int? LogId { get; set; }
    }
}
