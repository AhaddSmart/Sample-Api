using Domain.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [CreateService(false)]
    public class FileRepo : BaseEntity<int>
    {
        [Column("FileName")]
        [MaxLength(200)]
        public string fileName { get; set; }

        [Column("FilePath")]
        [MaxLength(500)]
        public string filePath { get; set; }

        [Column("FilePosition")]
        public int filePosition { get; set; }

        [Column("TableRef")]
        public FileRepositoryTableRef? TableRef { get; set; }

        [Column("TableRefID")]
        public int tableRefID { get; set; }

        [Column("Type")]
        public string type { get; set; }
    }
}





