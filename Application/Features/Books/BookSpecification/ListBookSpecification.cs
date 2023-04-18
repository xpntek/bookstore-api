using Application.Specifications;
using Domain;

namespace Application.Features.Books.BookSpecification;

public class ListBookSpecification : BaseSpecification<Book>
{
    public ListBookSpecification() 
    {
        AddInclude(book => book.Category );
    }
}