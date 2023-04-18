using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Authors;

public class UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result<AuthorDto>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }
    }

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AuthorDto>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
            if (author is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }
            
            author.FullName = request.FullName;
            author.Biography = request.Biography;
            author.Gender = request.Gender;
            author.Nationality = request.Nationality;
            
            _unitOfWork.Repository<Author>().Update(author);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError("Author don't save");
            }

            return _mapper.Map<AuthorDto>(author);
        }
    }
}