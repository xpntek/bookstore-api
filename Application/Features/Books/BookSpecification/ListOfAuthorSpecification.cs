using Application.Specifications;
using Domain;

namespace Application.Features.Books.BookSpecification;

public class ListOfAuthorSpecification:BaseSpecification<Author>
{
    public ListOfAuthorSpecification( List<int> authorId) : base(author =>authorId.Contains(author.Id) )
    {
       
    }
}