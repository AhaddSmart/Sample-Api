
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Services.Enquiries.Commands;

public class DeleteEnquiryCommand : IRequest<ResponseHelper>
{
    public int id { get; set; }
}
public class DeleteEnquiryCommandHandler : IRequestHandler<DeleteEnquiryCommand, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteEnquiryCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(DeleteEnquiryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objenquiry = await _context.Enquiries
              .FindAsync(new object[] { request.id }, cancellationToken);
            if (objenquiry != null)
            {
                _context.Enquiries.Remove(objenquiry);
                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseHelper(1, ResponseHelper.getObject("message", @"enquiry has been deleted"), new ErrorDef(0, string.Empty, string.Empty));
                
            }
            else
            {
                return new ResponseHelper(0, new object (), new ErrorDef(0, "404 not found", @"enquiry not Found"));
            }
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object (), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }

}


