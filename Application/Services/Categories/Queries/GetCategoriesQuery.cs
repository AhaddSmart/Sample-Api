using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.DTOs.CategoryDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Categories.Queries;

public class GetCategoryQuery : IRequest<ResponseHelper>
{
}
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
        var result = await _context.Categories
            //.Where(x=> x.ParentCategoryId == null)
            .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider);

            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));

        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));

        }

    }

}
