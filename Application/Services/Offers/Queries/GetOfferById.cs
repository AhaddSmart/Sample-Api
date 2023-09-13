using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs.BannerDto;
using Application.DTOs.NewsDtos;
using Application.DTOs.OfferDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Offers.Queries;

public class GetOfferByIdQuery : IRequest<ResponseHelper>
{
    public int Id { get; set; }
}
public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOfferByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //var entity = await _context.Offers.FindAsync(request.Id);

            //if (entity == null)
            //{
            //    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
            //}

            //var result = _mapper.Map<OfferDto>(entity);
            //return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

            var result = await _context.Offers
                .Where(x => request.Id == x.Id)
                .ProjectToListAsync<OfferDto>(_mapper.ConfigurationProvider);

            if (result.Count > 0)
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            else
                return new ResponseHelper(0, new object(), new ErrorDef(404, @"Error", "Not found", @"error"));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
