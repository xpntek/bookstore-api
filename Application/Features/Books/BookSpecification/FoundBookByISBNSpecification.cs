using Application.Specifications;
using Domain;
using Microsoft.Extensions.Options;

namespace Application.Features.Books.BookSpecification;

public class FoundBookByISBNSpecification : BaseSpecification<BookAuthor>
{
    public FoundBookByISBNSpecification(string ISBN):base(book => book.Book.ISBN==ISBN)
    {
        AddInclude( author =>author.Book.Category );
    }
}
