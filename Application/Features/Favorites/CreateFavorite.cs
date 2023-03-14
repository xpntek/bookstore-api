using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Favorites;

public class CreateFavorite
{
    public class  CreateFavoriteCommand : IRequest<Result<FavoriteDto>>
    {
        public int BookId { get; set; }
    }
    
    public class  CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand,Result<FavoriteDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public CreateFavoriteCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<Result<FavoriteDto>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favoriteSpec = new FoundFavoriteByBookIdSpecification(request.BookId);
            var favorite = await _unitOfWork.Repository<Favorite>().GetEntityWithSpec(favoriteSpec);
            if (favorite is not null)
            {
                return Results.ConflictError(" Favorite book Id:" + request.BookId);
            }

            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
            if (book is null)
            {
                return Results.NotFoundError(" Id:" + request.BookId);
            }

            var clientSpec = new FoundClientByIdSpecification(_userAccessor.GetCurrentUserID());
            var client = await _unitOfWork.Repository<Client>().GetEntityWithSpec(clientSpec);
            if (client is  null)
            {
                return Results.NotFoundError(" User with Id:" + _userAccessor.GetCurrentUserID());
            }
            
            var newFavorite = new Favorite()
            {
                BookId = request.BookId,
                UserId = _userAccessor.GetCurrentUserID()
            };
            
            _unitOfWork.Repository<Favorite>().Add(newFavorite);
            
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError("Fail to add favorite");
            }

            return _mapper.Map<FavoriteDto>(newFavorite);
        }
    }
}