﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTOs.NewsDtos;
using AutoMapper;
using MediatR;

namespace Application.Services.News.Queries;

public class GetNewsByIdQuery : IRequest<ResponseHelper>
{
    public int Id { get; set; }
}
public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, ResponseHelper>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNewsByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseHelper> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.News.FindAsync(request.Id);

            if (entity == null)
            {
                return new ResponseHelper(0, true, new ErrorDef(-1, "404 not found", "News not found"));
            }

            var result = _mapper.Map<NewsDto>(entity);
            return new ResponseHelper(1, result, new ErrorDef(0, string.Empty, string.Empty));
        }
        catch (Exception ex)
        {
            return new ResponseHelper(0, new object(), new ErrorDef(-1, @"Error", ex.Message, @"error"));
        }
    }
}
