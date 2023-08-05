using Application.Common.Interfaces;
using Application.DTOs.NewsDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.News.Commands
{
    public class CreateNewsCommand : IRequest<NewsDto>
    {
        public DateTime NewsDate { get; set; }
        public string Title { get; set; }
        public string FileRepoId { get; set; }
        public string NewsContent { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }

    }

    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, NewsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NewsDto> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.News
            {
                NewsDate = request.NewsDate,
                Title = request.Title,
                FileRepoId = request.FileRepoId,
                NewsContent = request.NewsContent,
                ValidFrom = request.ValidFrom,
                ValidTill = request.ValidTill
            };

            await _context.News.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NewsDto>(entity);
        }
    }
}
