using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class LogEntry : BaseEntity<int>
{
    [Column("LogLevel")]
    public string logLevel { get; set; }
    [Column("Message")]
    public string message { get; set; }
}

