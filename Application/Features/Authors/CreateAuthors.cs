using Application.Dtos;
using Application.Errors;
using Application.Features.Authors.AuthorSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Persistence;

namespace Application.Features.Authors;

public class CreateAuthors
{
    public class CreateAuthorsCommand : IRequest<Result<AuthorDto>>
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }
    }

    public class CreateAuthorsCommandHandler : IRequestHandler<CreateAuthorsCommand, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CreateAuthorsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<AuthorDto>> Handle(CreateAuthorsCommand request, CancellationToken cancellationToken)
        {
            var authorSpec = new FoundAuthorByFullNameSpecification(request.FullName);
            var authorObject = await  _unitOfWork.Repository<Author>().GetEntityWithSpec(authorSpec);

            if (authorObject is not null)
            {
                return Results.ConflictError(" this author already");
            }
            
            var author = new Author()
            {
                FullName = request.FullName,
                Biography = request.Biography,
                Gender = request.Gender,
                Nationality = request.Nationality
            };

            _unitOfWork.Repository<Author>().Add(author);
            var result = await _unitOfWork.Complete() < 0;
            if (result)
            {
                return Results.InternalError("Author don't save");
            }

            return _mapper.Map<AuthorDto>(author);
        }
    }
}