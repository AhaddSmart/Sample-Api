using Application.Common.Interfaces;
using Application.DTOs.CategoryDtos;
using Application.DTOs.NewsDtos;
using Application.Services.News.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int? ParentCategoryId { get; set; } = null;

    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
                var entity = new Domain.Entities.Category
                {
                    Name = request.Name,
                    Code = request.Code,
                    IsActive = request.IsActive,
                    ParentCategoryId = request.ParentCategoryId,

                };
            await _context.Categories.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(entity);
        }
    }
}
