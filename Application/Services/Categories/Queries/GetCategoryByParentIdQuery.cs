using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.CategoryDtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Queries;

public class GetCategoryByParentIdQuery : IRequest<List<CategoryDto>>
{
    public int ParentCategoryId { get; set; }
}
public class GetCategoryByParentIdQueryHandler : IRequestHandler<GetCategoryByParentIdQuery, List<CategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByParentIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var entity = _context.Categories.Where(c => c.ParentCategoryId == request.ParentCategoryId).ToList();

        if (entity == null)
        {
            throw new NotFoundException("Category entity not found");
        }

        return _mapper.Map<List<CategoryDto>>(entity);
    }
}
