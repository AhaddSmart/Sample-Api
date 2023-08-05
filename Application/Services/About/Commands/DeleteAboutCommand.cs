using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Services.About.Commands
{
    public class DeleteAboutCommand : IRequest<bool>
    {
        public int Id { get; set; } // Assuming you have an Id property to identify the About entity to delete
    }

    public class DeleteAboutCommandHandler : IRequestHandler<DeleteAboutCommand, bool>
    {
     
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteAboutCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Abouts.FindAsync(request.Id);

            if (entity == null)
            {
                // Handle the case where the About entity with the given Id is not found.
                // You can throw an exception or return an error response.
                throw new NotFoundException("About entity not found.");
            }

            // Update the properties of the About entity

            _context.Abouts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            //return _mapper.Map<AboutDto>(entity);
            return true;
        }
    }
}
