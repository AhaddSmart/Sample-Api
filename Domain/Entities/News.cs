

namespace Domain.Entities;
public class News : BaseEntity<int>
{
    public DateTime NewsDate { get; set; }
    public string Title { get; set; }
    public int? FileRepoId { get; set; }
    public virtual FileRepo? FileRepo { get; set; }
    public string NewsContent { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTill { get; set; }
}



