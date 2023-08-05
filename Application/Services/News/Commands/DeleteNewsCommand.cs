using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.News.Commands
{
    public class DeleteNewsCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }

    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, bool>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.News.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException("News entity not found.");
            }


            _context.News.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
