
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Application.Common.Models;
using Application.DTOs.VenderDtos;

namespace Application.Services.Vendors.Queries;
public class GetVendorsQuery : IRequest<ResponseHelper>
{

}

public class GetVendorsQueryHandler : IRequestHandler<GetVendorsQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVendorsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
       var result =  await _context.Vendors
            //.OrderByDescending(x => x.Id)
            .ProjectToListAsync<VendorDto>(_mapper.ConfigurationProvider);

        return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
    }
}


