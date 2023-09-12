using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BusinessCategories.Commands;
public class DeleteBusinessTypeCommand : IRequest<ResponseHelper>
{
    public int Id { get; set; }
}

public class DeleteBusinessTypeCommandHandler : IRequestHandler<DeleteBusinessTypeCommand, ResponseHelper>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBusinessTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseHelper> Handle(DeleteBusinessTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.BusinessTypes.FindAsync(request.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "Category not found"));
            }

            var hasChildTypes = await _context.BusinessTypes
            .AnyAsync(bt => bt.parentTypeId == entity.Id, cancellationToken);

            if (hasChildTypes)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(0, "400", "This Business Type has child types"));
            }
            else
            {
                _context.BusinessTypes.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));
            }

            //if (entity.parentTypeId != null)
            //{
            //    return new ResponseHelper(0, new object(), new ErrorDef(0, "400", "This Business Type have child Type"));
            //}
            //else
            //{
            //    _context.BusinessTypes.Remove(entity);
            //    await _context.SaveChangesAsync(cancellationToken);
            //    return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));
            //}
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
