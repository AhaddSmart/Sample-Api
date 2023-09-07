using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Helpers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Offers.Commands;

public class DeleteOfferCommand : IRequest<ResponseHelper>
{
    public int Id { get; set; }
}

public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, ResponseHelper>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteOfferCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseHelper> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ImageRepositoryHelper imageRepositoryHelper = new(_context);
            var entity = await _context.Offers.FindAsync(request.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
            }

            await imageRepositoryHelper.DeleteImage(entity.fileRepoId.Value, cancellationToken);
            _context.Offers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}