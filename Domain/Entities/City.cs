using Domain.Common;
using Domain.Helper;

namespace Domain.Entities
{
    [CreateService(false)]
    public class City : BaseEntity<int>/*, IHasDomainEvent*/
    {
        //public City()
        //{
        //Add List to be available with Class
        //E.g
        //Districts = new HashSet<District>();
        //}
        //[Required]
        public string? Name { get; set; }
        //Uncomment this line if the list is to be avaliable/return with city object
        //public ICollection<District> Districts { get; private set; }

        //public List<DomainEvent> DomainEvents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

