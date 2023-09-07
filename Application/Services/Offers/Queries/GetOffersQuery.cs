using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs.OfferDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Offers.Queries;

public class GetOffersQuery : IRequest<ResponseHelper>
{
}
public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOffersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetOffersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _context.Offers
                 .ProjectToListAsync<OfferDto>(_mapper.ConfigurationProvider);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }

    }

}
