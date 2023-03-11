using Application.Dtos;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using Domain;

namespace Infrastruture.Services;

public class ListOfAuthorsService: IListOfAuthorsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ListOfAuthorsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<BookAuthor> GetAuthors(int bookId)
    {
        var authorSpecification = new FoundBookAuthorByBookIdSpecification(bookId);
        var authors =  _unitOfWork.Repository<BookAuthor>()
            .ListWithSpecAsync(authorSpecification).GetAwaiter().GetResult();

        var authorList = authors.Select(author => new BookAuthor()
        {
            Author = author.Author,
        }).ToList();

        return authorList;
    }
}