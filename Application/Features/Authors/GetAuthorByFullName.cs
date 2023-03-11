using Application.Dtos;
using Application.Errors;
using Application.Features.Authors.AuthorSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Authors;

public class GetAuthorByFullName
{
    public class GetAuthorByFullNameQuery : IRequest<Result<AuthorDto>>
    {
        public string FullName { get; set; }
    }

    public class GetAuthorByFullNameQueryHandler : IRequestHandler<GetAuthorByFullNameQuery, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthorByFullNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AuthorDto>> Handle(GetAuthorByFullNameQuery request, CancellationToken cancellationToken)
        {
            var authorSpec = new FoundAuthorByFullNameSpecification(request.FullName);
            var authorObject = await  _unitOfWork.Repository<Author>().GetEntityWithSpec(authorSpec);
            if (authorObject is null)
            {
                return Results.NotFoundError(" this author ");
            }

            return _mapper.Map<AuthorDto>(authorObject);
        }
    }
}