using Application.Specifications;
using Domain;

namespace Application.Features.Categories.Specification;

public class FoundCategoryByDescriptionSpecification : BaseSpecification<Category>
{
    public FoundCategoryByDescriptionSpecification( string description) : base(category =>category.Description== description )
    {
        
    }
}