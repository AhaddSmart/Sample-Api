
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Application.Common.Models;
using Application.DTOs.VenderDtos;

namespace Application.Services.Vendors.Queries;
public class GetVendorQueryByID : IRequest<ResponseHelper>
{
   public int Id { get; set; }
}

public class GetVendorQueryByIDHandler : IRequestHandler<GetVendorQueryByID, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVendorQueryByIDHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetVendorQueryByID request, CancellationToken cancellationToken)
    {
       var result =  await _context.Vendors
            .Where(x => x.Id == request.Id)
            .ProjectToListAsync<VendorDtoByID>(_mapper.ConfigurationProvider);

        return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
    }
}


