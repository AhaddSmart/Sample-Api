using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.DTOs;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.About.Queries;

public class GetAboutQuery : IRequest<IEnumerable<AboutDto>>
{
}
public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, IEnumerable<AboutDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAboutQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AboutDto>> Handle(GetAboutQuery request, CancellationToken cancellationToken)
    {
        return await _context.Abouts
             .ProjectToListAsync<AboutDto>(_mapper.ConfigurationProvider);

    }

}
