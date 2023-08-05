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
using Microsoft.Extensions.Primitives;
using Application.Common.Models;

namespace Application.Services.News.Commands
{
    public class UpdateNewsCommand : IRequest<ResponseHelper>
    {
        public int Id { get; set; } // Assuming you have an Id property to identify the News entity
        public DateTime NewsDate { get; set; }
        public string Title { get; set; }
        public string FileRepoId { get; set; }
        public string NewsContent { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
    }

    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var entity = await _context.News.FindAsync(request.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));

                }

                entity.NewsDate = request.NewsDate;
                entity.Title = request.Title;
                entity.FileRepoId = request.FileRepoId;
                entity.NewsContent = request.NewsContent;
                entity.ValidFrom = request.ValidFrom;
                entity.ValidTill = request.ValidTill;

                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<NewsDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
