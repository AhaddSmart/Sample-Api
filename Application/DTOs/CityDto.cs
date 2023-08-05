using Application.Common.Mappings;
using Domain.Entities.Sample;
using AutoMapper;

namespace Application.DTOs
{
    public class CityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<City, CityDto>();
        }
    }

}
