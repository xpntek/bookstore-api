using Application.Specifications;
using Domain;

namespace Application.Features.Books.BookSpecification;

public class ListBookAuthorSpecification : BaseSpecification<BookAuthor>
{
    public ListBookAuthorSpecification() : base()
    {
        AddInclude(bookAuthor =>bookAuthor.Author );
        AddInclude(bookAuthor =>bookAuthor.Book );
    }
}