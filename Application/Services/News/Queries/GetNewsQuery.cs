using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.DTOs;
using Application.DTOs.NewsDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.News.Queries;

public class GetNewsQuery : IRequest<IEnumerable<NewsDto>>
{
}
public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, IEnumerable<NewsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNewsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NewsDto>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        return await _context.News
             .ProjectToListAsync<NewsDto>(_mapper.ConfigurationProvider);

    }

}
