using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using Application.Helpers;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Application.Services.News.Commands
{
    public class CreateNewsCommand : IRequest<ResponseHelper>
    {
        public HttpRequest formRequest { get; set; }
    }

    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string jsonString = request.formRequest.Form["JsonString"];

                CreateNewsDto objCreateNewsDto = JsonConvert.DeserializeObject<CreateNewsDto>(jsonString);


                var entity = new Domain.Entities.News
                {
                    NewsDate = objCreateNewsDto.NewsDate,
                    Title = objCreateNewsDto.Title,
                    FileRepoId = objCreateNewsDto.FileRepoId,
                    NewsContent = objCreateNewsDto.NewsContent,
                    ValidFrom = objCreateNewsDto.ValidFrom,
                    ValidTill = objCreateNewsDto.ValidTill
                };

                await _context.News.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var uploads = "Resources/uploads/News";

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                //ImageRepositoryHelper imageRepositoryHelper = new(_context);

                //IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
                //string fileName = FileRepositoryTableRef.News + "_" + entity.Id;
                //int Position = 1;
                //if (File != null)
                //{
                //    int ImageRepoId = await imageRepositoryHelper.SaveImage(File, fileName, Position, FileRepositoryTableRef.News, entity.Id, cancellationToken);

                //    if (ImageRepoId > 0)
                //    {
                //        var ItemData = await _context.News
                //            .FindAsync(new object[] { entity.Id }, cancellationToken);

                //        if (ItemData != null)
                //        {
                //            ItemData.itemImage_FileRepo = ImageRepoId;
                //        }
                //    }
                //}
                await _context.SaveChangesAsync(cancellationToken);

                var result =  _mapper.Map<NewsDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
