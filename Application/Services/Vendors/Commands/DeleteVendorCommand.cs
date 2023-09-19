
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using Application.Helpers;
using AutoMapper;
using Domain.Enums;
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
            ImageRepositoryHelper imageRepositoryHelper = new(_context);
            var objvendor = await _context.Vendors
              .FindAsync(new object[] { request.id }, cancellationToken);
            if (objvendor != null)
            {
                var recordsToRemove = _context.FileRepos
                    .Where(fr => fr.TableRef == FileRepositoryTableRef.VendorDocs && fr.tableRefID == request.id);
                //.ToList();
                foreach (var item in recordsToRemove)
                {
                    await imageRepositoryHelper.DeleteImage(item.Id, cancellationToken);
                }
                await imageRepositoryHelper.DeleteImage(objvendor.logoId.Value, cancellationToken);
                _context.Vendors.Remove(objvendor);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseHelper(1, ResponseHelper.getObject("message", @"vendor has been deleted"), new ErrorDef(0, string.Empty, string.Empty));

            }
            else
            {
                return new ResponseHelper(0, new object(), new ErrorDef(0, "404 not found", @"vendor not Found"));
            }
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }

}


