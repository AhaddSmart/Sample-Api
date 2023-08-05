

namespace Domain.Entities;
public class News : BaseEntity<int>
{
    public DateTime NewsDate { get; set; }
    public string Title { get; set; }
    public string? FileRepoId{ get; set; }
    public string NewsContent{ get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTill { get; set; }
}



