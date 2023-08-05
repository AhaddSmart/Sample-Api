using Application.Common.Exceptions;
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

public class GetNewsByIdQuery : IRequest<NewsDto>
{
    public int Id { get; set; }
}
//public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, IEnumerable<NewsDto>>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public GetNewsByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<IEnumerable<NewsDto>> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
//    {
//        var entity = await _context.News.FindAsync(request.Id);

//        if (entity == null)
//        {
//            throw new NotFoundException("News entity not found.");
//        }
//        return (IEnumerable<NewsDto>)entity;
//       // return await _context.News
//             //.ProjectToListAsync<NewsDto>(_mapper.ConfigurationProvider);

//    }

//}
public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNewsByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NewsDto> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.News.FindAsync(request.Id);

        if (entity == null)
        {
            return null; // Return null if the news item with the specified ID is not found.
        }

        return _mapper.Map<NewsDto>(entity);
    }
}
