using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.CategoryDtos;
using Application.DTOs.NewsDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services.Categories.Commands;

public class UpdateCategoryCommand : IRequest<ResponseHelper>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; }
    public int? ParentCategoryId { get; set; }
}

public class UpdateNewsCommandHandler : IRequestHandler<UpdateCategoryCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateNewsCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var entity = await _context.Categories.FindAsync(request.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "Category not found"));
            }

            entity.Name = request.Name;
            entity.Code = request.Code;
            entity.IsActive = request.IsActive;
            entity.ParentCategoryId = request.ParentCategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<CategoryDto>(entity);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
