using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.BannerDto;
using Application.Helpers;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Banner.Commands;
public class CreateBannerCommand : IRequest<ResponseHelper>
{
    public HttpRequest formRequest { get; set; }
}

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateBannerCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string jsonString = request.formRequest.Form["JsonString"];

            CreateBannerDto objCreateBannerDto = JsonConvert.DeserializeObject<CreateBannerDto>(jsonString);
            if (objCreateBannerDto.to > objCreateBannerDto.from)
            {
                var entity = new Domain.Entities.Banner
                {
                    //newsDate = objCreateOfferDto.newsDate,
                    title = objCreateBannerDto.title,
                    //newsContent = objCreateOfferDto.newsContent,
                    from = objCreateBannerDto.from,
                    to = objCreateBannerDto.to,
                };

                await _context.Banners.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var uploads = "Resources/uploads/Banners";

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                ImageRepositoryHelper imageRepositoryHelper = new(_context);

                IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
                string fileName = FileRepositoryTableRef.Banners + "_" + entity.Id;
                int Position = 1; //dout
                if (File != null)
                {
                    int ImageRepoId = await imageRepositoryHelper.SaveImage(File, fileName, Position, FileRepositoryTableRef.Banners, entity.Id, cancellationToken);

                    if (ImageRepoId > 0)
                    {
                        var ItemData = await _context.Banners
                            .FindAsync(new object[] { entity.Id }, cancellationToken);

                        if (ItemData != null)
                        {
                            ItemData.fileRepoId = ImageRepoId;
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<CreateBannerDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", "to date must greater then from date", @"error"));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
