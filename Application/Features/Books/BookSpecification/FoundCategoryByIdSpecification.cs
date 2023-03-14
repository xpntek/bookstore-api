using Application.Specifications;
using Domain;

namespace Application.Features.Books.BookSpecification;

public class FoundCategoryByIdSpecification : BaseSpecification<Category>
{
    public FoundCategoryByIdSpecification(int id) : base(category => category.Id == id)
    {
        
    }
}