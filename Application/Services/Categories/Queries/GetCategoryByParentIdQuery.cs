using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.CategoryDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries;

public class GetCategoryByParentIdQuery : IRequest<ResponseHelper>
{
    public int parentCategoryId { get; set; }
}
public class GetCategoryByParentIdQueryHandler : IRequestHandler<GetCategoryByParentIdQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByParentIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _context.Categories.Where(c => c.parentCategoryId == request.parentCategoryId).ToList();

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "Category not found"));
            }

            var result = _mapper.Map<List<CategoryDto>>(entity);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch(Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
