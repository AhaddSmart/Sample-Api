using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    //public string? ParentCategory { get; set; }
    public bool IsActive { get; set; } = true;
    public int? ParentCategoryId { get; set; }
    public virtual Category? ParentCategory { get; set; }
//    public ICollection<Category> ChildCategories { get; private set; } = new List<Category>();

}