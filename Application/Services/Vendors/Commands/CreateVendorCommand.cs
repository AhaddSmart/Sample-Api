using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using Application.DTOs.OfferDtos;
using Application.DTOs.VenderDtos;
using Application.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Offers.Commands;
public class CreateVendorCommand : IRequest<ResponseHelper>
{
    public HttpRequest formRequest { get; set; }
}

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateVendorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string jsonString = request.formRequest.Form["JsonString"];

            CreateVendorDto objCreateVendorDto = JsonConvert.DeserializeObject<CreateVendorDto>(jsonString);
                var entity = new Domain.Entities.Vendor
                {
                    name = objCreateVendorDto.name,
                    lic_no = objCreateVendorDto.lic_no,
                    taxNo = objCreateVendorDto.taxNo,
                    personName = objCreateVendorDto.personName,
                    designation = objCreateVendorDto.designation,
                    country = objCreateVendorDto.country,
                    city = objCreateVendorDto.city,
                    address = objCreateVendorDto.address,
                    email = objCreateVendorDto.email,
                    mobileNo = objCreateVendorDto.mobileNo,
                    website = objCreateVendorDto.website,
                    mobileNos = objCreateVendorDto.mobileNos,
                    emails = objCreateVendorDto.emails,
                };

                await _context.Vendors.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var uploads = "Resources/uploads/Vendors/Logos";

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                ImageRepositoryHelper imageRepositoryHelper = new(_context);

                IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
                string fileName = FileRepositoryTableRef.Logos + "_" + entity.Id;
                int Position = 1;
                if (File != null)
                {
                    int ImageRepoId = await imageRepositoryHelper.SaveImage(File, fileName, Position, FileRepositoryTableRef.Logos, entity.Id, cancellationToken);

                    if (ImageRepoId > 0)
                    {
                        var ItemData = await _context.Vendors
                            .FindAsync(new object[] { entity.Id }, cancellationToken);

                        if (ItemData != null)
                        {
                            ItemData.logoId = ImageRepoId;
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<CreateVenderDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
      
            //return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", "to date must greater then from date", @"error"));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}