using Application.Dtos;
using Domain;

namespace Application.Interfaces;

public interface IListOfAuthorsService
{
    List<BookAuthor> GetAuthors(int bookId);
}