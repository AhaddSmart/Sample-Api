using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using Application.DTOs.OfferDtos;
using Application.Helpers;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Offers.Commands;
public class CreateOfferCommand : IRequest<ResponseHelper>
{
    public HttpRequest formRequest { get; set; }
}

public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateOfferCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string jsonString = request.formRequest.Form["JsonString"];

            CreateOfferDto objCreateOfferDto = JsonConvert.DeserializeObject<CreateOfferDto>(jsonString);
            if (objCreateOfferDto.to > objCreateOfferDto.from) {
                var entity = new Domain.Entities.Offer
                {
                    title = objCreateOfferDto.title,
                    from = objCreateOfferDto.from,
                    to = objCreateOfferDto.to,
                };

                await _context.Offers.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var uploads = "Resources/uploads/Offers";

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                ImageRepositoryHelper imageRepositoryHelper = new(_context);

                IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
                string fileName = FileRepositoryTableRef.Offers + "_" + entity.Id;
                int Position = 1;
                if (File != null)
                {
                    int ImageRepoId = await imageRepositoryHelper.SaveImage(File, fileName, Position, FileRepositoryTableRef.Offers, entity.Id, cancellationToken);

                    if (ImageRepoId > 0)
                    {
                        var ItemData = await _context.Offers
                            .FindAsync(new object[] { entity.Id }, cancellationToken);

                        if (ItemData != null)
                        {
                            ItemData.fileRepoId = ImageRepoId;
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<CreateOfferDto>(entity);
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