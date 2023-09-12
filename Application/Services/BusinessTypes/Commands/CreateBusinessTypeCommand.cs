using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.BusinessTypeyDtos;
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

namespace Application.Services.BusinesTypes.Commands
{
    public class CreateBusinessTypeCommand : IRequest<ResponseHelper>
    {
        public string name { get; set; }
        public string code { get; set; }
        public bool isActive { get; set; }
        public int? parentTypeId { get; set; } = null;

    }
    public class CreateBusinessTypeCommandHandler : IRequestHandler<CreateBusinessTypeCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBusinessTypeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(CreateBusinessTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new Domain.Entities.BusinessType
                {
                    name = request.name,
                    code = request.code,
                    isActive = request.isActive,
                    parentTypeId = request.parentTypeId,
                };
                await _context.BusinessTypes.AddAsync(entity, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                var result = _mapper.Map<BusinessTypeDto>(entity);

                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}
