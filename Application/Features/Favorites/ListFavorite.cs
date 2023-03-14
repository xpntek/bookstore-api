using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Favorites;

public class ListFavorite
{
    public class  ListFavoriteQuery : IRequest<Result<List<BookDto>>>
    {
        
    }
    public class ListFavoriteQueryHandler : IRequestHandler<ListFavoriteQuery,Result<List<BookDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public ListFavoriteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }
        public async Task<Result<List<BookDto>>> Handle(ListFavoriteQuery request, CancellationToken cancellationToken)
        {
            var favoriteListSpec = new ListFavoriteSpecification(_userAccessor.GetCurrentUserID());
            var favoriteList = await _unitOfWork.Repository<Favorite>().ListWithSpecAsync(favoriteListSpec);
            var bookList = new List<Book>();
            foreach (var favorite in favoriteList)
            {
                bookList.Add(favorite.Book);   
            }
            return _mapper.Map<List<BookDto>>(bookList);
        }
    }
}