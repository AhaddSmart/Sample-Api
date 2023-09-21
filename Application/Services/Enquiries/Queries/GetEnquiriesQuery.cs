
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Application.Common.Models;
using Application.DTOs.EnquiryDtos;

namespace Application.Services.Enquiries.Queries;
public class GetEnquiriesQuery : IRequest<ResponseHelper>
{

}

public class GetEnquiriesQueryHandler : IRequestHandler<GetEnquiriesQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEnquiriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetEnquiriesQuery request, CancellationToken cancellationToken)
    {
       var result =  await _context.Enquiries
            .OrderByDescending(x => x.Id)
            .ProjectToListAsync<EnquiryDto>(_mapper.ConfigurationProvider);

        return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
    }
}


