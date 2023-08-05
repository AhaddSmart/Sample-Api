using Application.Common.Interfaces;
using Application.DTOs;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;

namespace Application.Services.Cities.Queries;
public class GetCitiesQuery : IRequest<IEnumerable<CityDto>>
{
    public string CityName { get; set; }
}
public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<CityDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cities
            .OrderBy(x => x.Name)
            .ProjectToListAsync<CityDto>(_mapper.ConfigurationProvider);
    }
}
