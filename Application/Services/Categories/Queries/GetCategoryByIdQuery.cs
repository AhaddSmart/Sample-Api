using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.CategoryDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<ResponseHelper>
{
    public int Id { get; set; }
}
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.Categories.FindAsync(request.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "Category not found"));

            }
            var result = _mapper.Map<CategoryDto>(entity);

            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
