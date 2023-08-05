using Application.DTOs;
using MediatR;
using AutoMapper ;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.Sample;

namespace Application.Services.Cities.Commands
{
    public class CreateCityCommand : IRequest<CityDto>
    {
        public string? Name { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = new City
            {
                Name = request.Name
            };

            await _context.Cities.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CityDto>(entity);
        }
    }
}
