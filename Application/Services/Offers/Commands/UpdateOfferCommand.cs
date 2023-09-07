using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using Application.DTOs.OfferDtos;
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

namespace Application.Services.Offers.Commands;
public class UpdateOfferCommand : IRequest<ResponseHelper>
{
    public HttpRequest formRequest { get; set; }
}

public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOfferCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string jsonString = request.formRequest.Form["JsonString"];

            UpdateOfferDto objUpdateOfferDto = JsonConvert.DeserializeObject<UpdateOfferDto>(jsonString);

            var entity = await _context.Offers.FindAsync(objUpdateOfferDto.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
            }

            entity.title = objUpdateOfferDto.title;
            //entity.fileRepoId = objUpdateOfferDto.fileRepoId;
            entity.from = objUpdateOfferDto.from;
            entity.to = objUpdateOfferDto.to;

            ImageRepositoryHelper imageRepositoryHelper = new(_context);

            IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
            string fileName = FileRepositoryTableRef.Offers + "_" + entity.Id;
            int Position = 1; //dout
            if (File != null)
            {
                int ImageRepoId = await imageRepositoryHelper.UpdateImage(entity.fileRepoId.Value, File, fileName, Position, FileRepositoryTableRef.Offers, objUpdateOfferDto.Id, cancellationToken);

                if (ImageRepoId > 0)
                {
                    entity.fileRepoId = ImageRepoId;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            //file work
            var result = _mapper.Map<UpdateOfferDto>(entity);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}