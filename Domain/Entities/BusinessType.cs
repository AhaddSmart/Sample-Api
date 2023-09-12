using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class BusinessType : BaseEntity<int>
{
    [Column("Name")]
    public string name { get; set; }
    [Column("Code")]
    public string code { get; set; }
    [Column("IsActive")]
    public bool isActive { get; set; } = true;
    [Column("ParentTypeId")]
    public int? parentTypeId { get; set; }
    [Column("ParentType")]
    public virtual BusinessType? ParentType { get; set; }
}