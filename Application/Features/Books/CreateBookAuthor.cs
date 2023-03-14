using Application.Dtos;
using Application.Errors;
using Application.Features.Books.BookSpecification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Persistence;

namespace Application.Features.Books;

public class CreateBookAuthor
{
    public class CreateBookCommand : IRequest<Result<BookDto>>
    {
        public string Title { get; set; }
        public string PublishingCompany { get; set; }
        public string ISBN { get; set; }
        public string PublishingYear { get; set; }
        public string Edition { get; set; }
        public string PagesNumbers { get; set; }
        public int CategoryId { get; set; }
        public string Synopsis { get; set; }
        public float Price { get; set; }
        public int BookAmount { get; set; }
        public string CoverUrl { get; set; }
        public int Rating { get; set; }
        public List<Author> Authors { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookDto>>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CreateBookCommandHandler(IUnitOfWork work, IMapper mapper, DataContext context)
        {
            _work = work;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookSpec = new FoundBookByISBNSpecification(request.ISBN);
            var bookObject = await _work.Repository<BookAuthor>().GetEntityWithSpec(bookSpec);
            if (bookObject is not null)
            {
                return Results.ConflictError(request.ISBN);
            }

            var listOfAuthorsModel = request.Authors;
            if (listOfAuthorsModel.Count == 0)
            {
                return Results.InternalError("The book don't have author");
            }

            var listOfAuthorsToValidateId = listOfAuthorsModel.Select(x => x.Id).ToList();

            var listOfAuthorsToValidateSpecification = new ListOfAuthorSpecification(listOfAuthorsToValidateId);

            var listOfAuthorsToValidate = await _work.Repository<Author>()
                .ListWithSpecAsync(listOfAuthorsToValidateSpecification);

            if (listOfAuthorsToValidate is null)
            {
                return Results.NotFoundError("The book don't have author");
            }

            var categorySpec = new FoundCategoryByIdSpecification(request.CategoryId);
            var category = await _work.Repository<Category>().GetEntityWithSpec(categorySpec);
            if (category is null)
            {
                return Results.NotFoundError("The category ");
            }

            await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var book = new Book()
                {
                    CategoryId = category.Id,
                    Edition = request.Edition,
                    Price = request.Price,
                    Rating = request.Rating,
                    Synopsis = request.Synopsis,
                    Title = request.Title,
                    BookAmount = request.BookAmount,
                    CoverUrl = request.CoverUrl,
                    PagesNumbers = request.PagesNumbers,
                    PublishingCompany = request.PublishingCompany,
                    PublishingYear = request.PublishingYear,
                    ISBN = request.ISBN,
                };
                _work.Repository<Book>().Add(book);
                var result = await _work.Complete();
                if (result < 0)
                {
                    return Results.InternalError("Fail to add book");
                }
                
                var bookAuthDto = new BookDto()
                {
                    CategoryId = request.CategoryId,
                    Category = book.Category.Description,
                    Edition = request.Edition,
                    Price = request.Price,
                    Rating = request.Rating,
                    Synopsis = request.Synopsis,
                    Title = request.Title,
                    BookAmount = request.BookAmount,
                    CoverUrl = request.CoverUrl,
                    PagesNumbers = request.PagesNumbers,
                    PublishingCompany = request.PublishingCompany,
                    PublishingYear = request.PublishingYear,
                    ISBN = request.ISBN
                };

                var bookAuthorsList = new List<BookAuthor>();
                foreach (var author in listOfAuthorsModel)
                {
                    var bookAuthors = new BookAuthor()
                    {
                        AuthorId = author.Id,
                        BookId = book.Id,
                    };
                    bookAuthorsList.Add(bookAuthors);
                    _work.Repository<BookAuthor>().Add(bookAuthors);
                    bookAuthDto.Authors.Add(author.FullName);
                }

                result = await _work.Complete();
                if (result < 0)
                {
                    return Results.InternalError("Fail to add book");
                }

                await transactionScope.CommitAsync(cancellationToken);
                return bookAuthDto;
            }
            catch (Exception e)
            {
                await transactionScope.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}