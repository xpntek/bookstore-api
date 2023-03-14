using Application.Dtos;
using Application.Errors;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Books;

public class ListBookById
{
    public class ListBookByIdQuery : IRequest<Result<BookDto>>
    {
        public int Id { get; set; }
    }
    public class ListBookByIdQueryHandler : IRequestHandler<ListBookByIdQuery,Result<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IListOfAuthorsService _listOfAuthorsService;

        public ListBookByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper, IListOfAuthorsService listOfAuthorsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _listOfAuthorsService = listOfAuthorsService;
        }
        public async Task<Result<BookDto>> Handle(ListBookByIdQuery request, CancellationToken cancellationToken)
        {

            var bookObject = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id);
            var bookSpec = new FoundBookByISBNSpecification(bookObject.ISBN);
            var bookAuthor = await _unitOfWork.Repository<BookAuthor>().GetEntityWithSpec(bookSpec);
            var bookAuthDto = new BookDto()
                {
                    CategoryId = bookAuthor.Book.CategoryId,
                    Category =  bookAuthor.Book.Category.Description,
                    Edition =  bookAuthor.Book.Edition,
                    Price =  bookAuthor.Book.Price,
                    Rating =  bookAuthor.Book.Rating,
                    Synopsis =  bookAuthor.Book.Synopsis,
                    Title =  bookAuthor.Book.Title,
                    BookAmount =  bookAuthor.Book.BookAmount,
                    CoverUrl =  bookAuthor.Book.CoverUrl,
                    PagesNumbers =  bookAuthor.Book.PagesNumbers,
                    PublishingCompany =  bookAuthor.Book.PublishingCompany,
                    PublishingYear =  bookAuthor.Book.PublishingYear,
                    ISBN =  bookAuthor.Book.ISBN
                };
                var authorList = _listOfAuthorsService.GetAuthors( bookAuthor.Book.Id);
                foreach (var  author  in authorList)
                {
                    bookAuthDto.Authors.Add(author.Author.FullName);
                }

                return bookAuthDto;

        }
    }
}