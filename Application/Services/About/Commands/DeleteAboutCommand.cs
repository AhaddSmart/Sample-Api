using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace Application.Services.About.Commands
{
    public class DeleteAboutCommand : IRequest<ResponseHelper>
    {
        public int Id { get; set; }
    }

    public class DeleteAboutCommandHandler : IRequestHandler<DeleteAboutCommand, ResponseHelper>
    {
     
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseHelper> Handle(DeleteAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Abouts.FindAsync(request.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "About not found"));

                }
                _context.Abouts.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));
            } 
            catch(Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
