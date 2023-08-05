using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.NewsDtos;

namespace Application.Services.News.Commands
{
    public class UpdateNewsCommand : IRequest<NewsDto>
    {
        public int Id { get; set; } // Assuming you have an Id property to identify the News entity
        public DateTime NewsDate { get; set; }
        public string Title { get; set; }
        public string FileRepoId { get; set; }
        public string NewsContent { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
    }

    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, NewsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NewsDto> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.News.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException("News entity not found.");
            }

            entity.NewsDate = request.NewsDate;
            entity.Title = request.Title;
            entity.FileRepoId = request.FileRepoId;
            entity.NewsContent = request.NewsContent;
            entity.ValidFrom = request.ValidFrom;
            entity.ValidTill = request.ValidTill;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NewsDto>(entity);
        }
    }
}
