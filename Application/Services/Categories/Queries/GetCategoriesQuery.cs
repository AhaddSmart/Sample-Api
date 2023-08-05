using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.DTOs.CategoryDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.Categories.Queries;

public class GetCategoryQuery : IRequest<IEnumerable<CategoryDto>>
{
}
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, IEnumerable<CategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            //.Where(x=> x.ParentCategoryId == null)
            .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider);
    }

}
