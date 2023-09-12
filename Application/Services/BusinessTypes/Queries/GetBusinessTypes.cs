using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs.BusinessTypeyDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BusinessCategories.Queries;


public class GetBusinessTypeQuery : IRequest<ResponseHelper>
{
}
public class GetBusinessTypeQueryHandler : IRequestHandler<GetBusinessTypeQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBusinessTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetBusinessTypeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.BusinessTypes
                .ProjectToListAsync<BusinessTypeDto>(_mapper.ConfigurationProvider);

            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));

        }

    }

}
