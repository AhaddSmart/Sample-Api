using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.About.Queries;

public class GetAboutQuery : IRequest<ResponseHelper>
{
}
public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAboutQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetAboutQuery request, CancellationToken cancellationToken)
    {
        string abc = "abc";
        int xyz = Convert.ToInt32(abc);
        try
        {
            var result = await _context.Abouts
                 .ProjectToListAsync<AboutDto>(_mapper.ConfigurationProvider);

            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }

    }

}
