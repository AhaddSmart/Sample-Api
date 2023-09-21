
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Application.Common.Models;
using Application.DTOs.EnquiryDtos;

namespace Application.Services.Enquiries.Queries;
public class GetEnquiryQueryByID : IRequest<ResponseHelper>
{
   public int Id { get; set; }
}

public class GetEnquiryQueryByIDHandler : IRequestHandler<GetEnquiryQueryByID, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEnquiryQueryByIDHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetEnquiryQueryByID request, CancellationToken cancellationToken)
    {
       var result =  await _context.Enquiries
            .Where(x => x.Id == request.Id)
            .ProjectToListAsync<EnquiryDtoByID>(_mapper.ConfigurationProvider);

        return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
    }
}


