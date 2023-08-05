using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities.Sample;
using MediatR;

namespace Application.Services.About.Commands
{
    public class CreateAboutCommand : IRequest<AboutDto>
    {
        public string? Text { get; set; }

    }

    public class CreateAboutCommandHandler : IRequestHandler<CreateAboutCommand, AboutDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AboutDto> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Sample.About
            {
                Text = request.Text
            };

            await _context.Abouts.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AboutDto>(entity);
        }
    }
}
