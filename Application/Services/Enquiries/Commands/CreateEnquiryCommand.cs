
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.EnquiryDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Enquiries.Commands;

public class CreateEnquiryCommand : CreateEnquiryDto, IRequest<ResponseHelper>
    {


    }
public class CreateEnquiryCommandHandler : IRequestHandler<CreateEnquiryCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateEnquiryCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(CreateEnquiryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Domain.Entities.Enquiry
            {
                title = request.title,
                    businessTypeId = request.businessTypeId,
                    loadingPortCountry = request.loadingPortCountry,
                    loadingPortCity = request.loadingPortCity,
                    unloadingPortCountry = request.unloadingPortCountry,
                    unloadingPortCity = request.unloadingPortCity,
                    detail = request.detail,
                    
            };

            await _context.Enquiries.AddAsync(entity, cancellationToken);


            await _context.SaveChangesAsync(cancellationToken);

            var result = new { Id = entity.Id };

            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object (), new ErrorDef(-1, @"Error", ex.Message, @"Error"));
        }
    }
}

