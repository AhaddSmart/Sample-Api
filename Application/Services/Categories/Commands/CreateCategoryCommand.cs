using Application.Common.Interfaces;
using Application.Common.Models;
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
    public class CreateCategoryCommand : IRequest<ResponseHelper>
    {
        public string name { get; set; }
        public string code { get; set; }
        public bool isActive { get; set; }
        public int? parentCategoryId { get; set; } = null;

    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Domain.Entities.Category
                {
                    name = request.name,
                    code = request.code,
                    isActive = request.isActive,
                    parentCategoryId = request.parentCategoryId,

                };
                await _context.Categories.AddAsync(entity, cancellationToken);

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
}
