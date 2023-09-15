using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Services.About.Commands
{
    public class UpdateAboutCommand : IRequest<ResponseHelper>
    {
        public int Id { get; set; } // Assuming you have an Id property to identify the About entity
        public string text { get; set; }
    }

    public class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, ResponseHelper>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAboutCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Abouts.FindAsync(request.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "About not found"));
                }

                entity.text = request.text;

                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<AboutDto>(entity);
                return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}

