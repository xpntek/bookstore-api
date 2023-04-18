using Application.Specifications;
using Domain;

namespace Application.Features.Books.BookSpecification;

public class FoundBookAuthorByBookIdSpecification : BaseSpecification<BookAuthor>
{
    public FoundBookAuthorByBookIdSpecification(int id):base(book => book.Book.Id==id)
    {
        AddInclude(bookAuthor => bookAuthor.Book);   
        AddInclude(bookAuthor => bookAuthor.Author);   
    }
}