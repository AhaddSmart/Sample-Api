using Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [CreateService(false)]
    public class LogEntry : BaseEntity<int>
    {
        [Column("LogLevel")]
        public string logLevel { get; set; }
        [Column("Message")]
        public string message { get; set; }
        [Column("RequestTime")]
        public DateTime requestTime { get; set; }
        [Column("ResponseTime")]
        public DateTime? responseTime { get; set; }
    }
}
