using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.VenderDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Vendors.Commands;

public class UpdateVendorCommand : UpdateVendorDto, IRequest<ResponseHelper>
{
//public int id { get; set; }

}
public class UpdateVendorCommandCommandHandler : IRequestHandler<UpdateVendorCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateVendorCommandCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var VendorData = await _context.Vendors
               .FindAsync(new object[] { request.id }, cancellationToken);
            if (VendorData != null)
            {
                VendorData.logoId = request.logoId;
                    VendorData.name = request.name;
                    VendorData.lic_no = request.lic_no;
                    VendorData.taxNo = request.taxNo;
                    VendorData.personName = request.personName;
                    VendorData.designation = request.designation;
                    VendorData.country = request.country;
                    VendorData.city = request.city;
                    VendorData.address = request.address;
                    VendorData.email = request.email;
                    VendorData.mobileNo = request.mobileNo;
                    VendorData.website = request.website;
                    VendorData.mobileNos = request.mobileNos;
                    VendorData.emails = request.emails;
                    

                await _context.SaveChangesAsync(cancellationToken);
                var result = new { Id = VendorData.Id };

                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            else
            {
                return new ResponseHelper(0, new object (), new ErrorDef(0, "404 not found", @"vendor not Found"));
            }

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object (), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}


