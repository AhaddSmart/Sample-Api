using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
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

public class GetNewsQuery : IRequest<ResponseHelper>
{
}
public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNewsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.News
                 .ProjectToListAsync<NewsDto>(_mapper.ConfigurationProvider);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }

    }

}
