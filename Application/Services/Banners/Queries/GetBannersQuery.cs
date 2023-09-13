using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs.BannerDto;
using Application.DTOs.OfferDtos;
using Application.Helper;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Banner.Queries;

public class GetBannersQuery : IRequest<ResponseHelper>
{
}
public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBannersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        var todaysDate = DateTimeHelper.GetStartOfDay(DateTime.Now);
        try
        {
            var result = await _context.Banners
                .Where(x => todaysDate >= x.from && todaysDate <= x.to)
                .ProjectToListAsync<BannerDto>(_mapper.ConfigurationProvider);
            if (result.Count == 0)
                return new ResponseHelper(0, new object(), new ErrorDef(404, @"Error", "Not found", @"error"));
            else
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }

    }

}