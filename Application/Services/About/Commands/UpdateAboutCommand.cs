using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.Sample;
using MediatR;

namespace Application.Services.About.Commands
{
    public class UpdateAboutCommand : IRequest<AboutDto>
    {
        public int Id { get; set; } // Assuming you have an Id property to identify the About entity
        public string Text { get; set; }
    }

    public class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, AboutDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AboutDto> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Abouts.FindAsync(request.Id);

            if (entity == null)
            {
                // Handle the case where the About entity with the given Id is not found.
                // You can throw an exception or return an error response.
                throw new NotFoundException("About entity not found.");
            }

            // Update the properties of the About entity
            entity.Text = request.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AboutDto>(entity);
        }
    }
}

