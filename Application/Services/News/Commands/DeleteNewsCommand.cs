using Application.Common.Exceptions;
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

namespace Application.Services.News.Commands
{
    public class DeleteNewsCommand : IRequest<ResponseHelper>
    {
        public int Id { get; set; } 
    }

    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, ResponseHelper>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseHelper> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ImageRepositoryHelper imageRepositoryHelper = new(_context);
                var entity = await _context.News.FindAsync(request.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
                }

                await imageRepositoryHelper.DeleteImage(request.Id, cancellationToken);
                _context.News.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));

            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
