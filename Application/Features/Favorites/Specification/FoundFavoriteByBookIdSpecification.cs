using Application.Specifications;
using Domain;

namespace Application.Features.Favorites;

public class FoundFavoriteByBookIdSpecification : BaseSpecification<Favorite>
{
    public FoundFavoriteByBookIdSpecification(int bookId) : base(favorite => favorite.Book.Id==bookId)
    {
        AddInclude(favorite => favorite.Book );
        AddInclude(favorite => favorite.User );
    }
}