
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Services.Vendors.Commands;

public class DeleteVendorCommand : IRequest<ResponseHelper>
{
    public int id { get; set; }
}
public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteVendorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objvendor = await _context.Vendors
              .FindAsync(new object[] { request.id }, cancellationToken);
            if (objvendor != null)
            {
                _context.Vendors.Remove(objvendor);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseHelper(1, ResponseHelper.getObject("message", @"vendor has been deleted"), new ErrorDef(0, string.Empty, string.Empty));

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


