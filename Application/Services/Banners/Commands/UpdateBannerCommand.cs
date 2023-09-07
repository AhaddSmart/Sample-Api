using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.BannerDto;
using Application.DTOs.NewsDtos;
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

namespace Application.Services.Banners.Commands;
public class UpdateBannerCommand : IRequest<ResponseHelper>
{
    public HttpRequest formRequest { get; set; }
}

public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBannerCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string jsonString = request.formRequest.Form["JsonString"];

            UpdateBannerDto objUpdateBannerDto = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonString);

            var entity = await _context.Banners.FindAsync(objUpdateBannerDto.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
            }

            entity.title = objUpdateBannerDto.title;
            //entity.fileRepoId = objUpdateOfferDto.fileRepoId;
            entity.from = objUpdateBannerDto.from;
            entity.to = objUpdateBannerDto.to;

            ImageRepositoryHelper imageRepositoryHelper = new(_context);

            IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
            string fileName = FileRepositoryTableRef.Banners + "_" + entity.Id;
            int Position = 1; //dout
            if (File != null)
            {
                int ImageRepoId = await imageRepositoryHelper.UpdateImage(entity.fileRepoId.Value, File, fileName, Position, FileRepositoryTableRef.Banners, objUpdateBannerDto.Id, cancellationToken);

                if (ImageRepoId > 0)
                {
                    entity.fileRepoId = ImageRepoId;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            //file work
            var result = _mapper.Map<UpdateBannerDto>(entity);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}