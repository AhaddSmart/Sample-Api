using Application.Common.Interfaces;
using Domain.Entities;
using AutoMapper;
using MediatR;
using Application.DTOs.NewsDtos;
using Application.Common.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Application.Helpers;
using Domain.Enums;

namespace Application.Services.News.Commands
{
    public class UpdateNewsCommand : IRequest<ResponseHelper>
    {
        public HttpRequest formRequest { get; set; }
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
                string jsonString = request.formRequest.Form["JsonString"];

                UpdateNewsDto objUpdateNewsDto = JsonConvert.DeserializeObject<UpdateNewsDto>(jsonString);

                var entity = await _context.News.FindAsync(objUpdateNewsDto.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));

                }
               
                entity.newsDate = objUpdateNewsDto.newsDate;
                entity.title = objUpdateNewsDto.title;
                entity.fileRepoId = objUpdateNewsDto.fileRepoId;
                entity.newsContent = objUpdateNewsDto.newsContent;
                entity.validFrom = objUpdateNewsDto.validFrom;
                entity.validTill = objUpdateNewsDto.validTill;

                ImageRepositoryHelper imageRepositoryHelper = new(_context);

                IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
                string fileName = FileRepositoryTableRef.News + "_" + entity.Id;
                int Position = 1;
                if (File != null)
                {
                    int ImageRepoId = await imageRepositoryHelper.UpdateImage(objUpdateNewsDto.fileRepoId, File, fileName, Position, FileRepositoryTableRef.News, objUpdateNewsDto.Id, cancellationToken);

                    if (ImageRepoId > 0)
                    {
                        entity.fileRepoId = ImageRepoId;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                //file work
                var result = _mapper.Map<UpdateNewsDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
