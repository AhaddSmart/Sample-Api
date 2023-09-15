using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Services.About.Commands
{
    public class CreateAboutCommand : IRequest<ResponseHelper>
    {
        public string? text { get; set; }

    }

    public class CreateAboutCommandHandler : IRequestHandler<CreateAboutCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Domain.Entities.About
                {
                    text = request.text
                };

                await _context.Abouts.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<AboutDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));

            }
        }
    }
}
