using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string FileName { get; set; } // Add property for file name
        public int LineNumber { get; set; }  // Add property for line number
        public string ClassName { get; set; } // Add property for class name
    }
}
