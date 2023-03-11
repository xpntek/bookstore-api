using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Authors;

public class DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Result<AuthorDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AuthorDto>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
            if (authors is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }

            _unitOfWork.Repository<Author>().Delete(authors);
            var result = await _unitOfWork.Complete();
            
            if (result < 0)
            {
                return Results.InternalError("Author don't  deleted");
            }

            return _mapper.Map<AuthorDto>(authors);
        }
    }
}