using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.EnquiryDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Enquiries.Commands;

public class UpdateEnquiryCommand : UpdateEnquiryDto, IRequest<ResponseHelper>
{
//public int id { get; set; }

}
public class UpdateEnquiryCommandCommandHandler : IRequestHandler<UpdateEnquiryCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEnquiryCommandCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(UpdateEnquiryCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var EnquiryData = await _context.Enquiries
               .FindAsync(new object[] { request.id }, cancellationToken);
            if (EnquiryData != null)
            {
                EnquiryData.title = request.title;
                    EnquiryData.businessTypeId = request.businessTypeId;
                    EnquiryData.loadingPortCountry = request.loadingPortCountry;
                    EnquiryData.loadingPortCity = request.loadingPortCity;
                    EnquiryData.unloadingPortCountry = request.unloadingPortCountry;
                    EnquiryData.unloadingPortCity = request.unloadingPortCity;
                    EnquiryData.detail = request.detail;
                    

                await _context.SaveChangesAsync(cancellationToken);
                var result = new { Id = EnquiryData.Id };

                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            else
            {
                return new ResponseHelper(0, new object (), new ErrorDef(0, "404 not found", @"enquiry not Found"));
            }

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object (), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}


