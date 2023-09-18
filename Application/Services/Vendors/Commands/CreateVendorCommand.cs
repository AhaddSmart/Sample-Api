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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Vendors.Commands;
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
                mobileNos = string.Join(" | ", objCreateVendorDto.mobileNos),
                emails = string.Join(" | ", objCreateVendorDto.emails),
            };

            await _context.Vendors.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var uploads = "Resources/uploads/Vendors/Logos";

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            ImageRepositoryHelper imageRepositoryHelper = new(_context);

            //IFormFile File = request.formRequest.Form.Files.Count() > 0 ? request.formRequest.Form.Files[0] : null;
            if (request.formRequest.Form.Files.Count() > 0)
            {
                for (int i = 0; i < request.formRequest.Form.Files.Count(); i++)
                {
                    IFormFile File = request.formRequest.Form.Files[i];
                    if (i != 0)
                    {
                        string fileName = FileRepositoryTableRef.VendorDocs + "_" + entity.Id;
                        int Position = 1;
                        if (File != null)
                        {
                            await imageRepositoryHelper.SaveImage(File, fileName, Position, FileRepositoryTableRef.VendorDocs, entity.Id, cancellationToken);

                        }
                    }
                    else
                    {
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
                    }
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            //var result = _mapper.Map<CreateVendorDto>(entity);
            var result = new { Id = entity.Id };
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

            //return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", "to date must greater then from date", @"error"));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}





//if (request.formRequest.Form.Files.Count() > 0)
//            {
//                foreach (var attachment in request.formRequest.Form.Files)
//                {
//                    try
//                    {
//                        if (attachment != null)
//                        {
//                            Guid newGuid = Guid.NewGuid();
//string ext = Path.GetExtension(attachment.FileName);
//string fileName = newGuid.ToString() + "." + ext;

//var filePath = Path.Combine(uploads, fileName);

//                            using (var fileStream = new FileStream(filePath, FileMode.Create))
//                            {
//                                await attachment.CopyToAsync(fileStream);
//                            }

//                            var entityFileRepo = new FileRepo
//                            {
//                                fileName = attachment.FileName,
//                                filePath = filePath,
//                                filePosition = 0,
//                                TabelRef = Domain.Enums.FileRepositoryTableRef.Lead,
//                                tableRefID = entity.Id,
//                                type = attachment.ContentType != null ? attachment.ContentType : "",
//                                masterCustomerId = objQuotationData.masterCustomerId,
//                            };

//await _context.FileRepos.AddAsync(entityFileRepo, cancellationToken);
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//}
//                }
//                await _context.SaveChangesAsync(cancellationToken);
//            }