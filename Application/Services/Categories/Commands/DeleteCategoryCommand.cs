using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.Services.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<ResponseHelper>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseHelper>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseHelper> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Categories.FindAsync(request.Id);

                if (entity == null)
                {
                    return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "Category not found"));
                }

                if (entity.parentCategoryId != null)
                {
                    return new ResponseHelper(0, new object(), new ErrorDef(0, "400", "Category have child Category"));
                }
                else
                {
                    _context.Categories.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new ResponseHelper(1, true, new ErrorDef(0, string.Empty, string.Empty));
                }
            }
            catch (Exception ex)
            {
                return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
            }
        }
    }
}

